namespace TwitterAnalysis.Core.Database
{
    using Gremlin.Net.Driver;
    using Gremlin.Net.Structure.IO.GraphSON;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TwitterAnalysis.Core.Models.Tweet;

    public class TweetGraphDatabase : ITweetGraphDatabase
    {
        private readonly IConfiguration configuration;

        private readonly GremlinServer server;

        public TweetGraphDatabase(IConfiguration configuration)
        {
            this.configuration = configuration;

            //server = new GremlinServer("", 443, enableSsl: true,
            //                                    username: "",
            //                                    password: "");

            server = new GremlinServer("localhost", 8182);
        }

        private static List<string> gremlinQueries = new List<string>
        {
            {"g.V().drop()" },
            {"g.addV('person').property('id', 'thomas').property('firstName', 'Thomas').property('age', 44).property('PartitionKey', 'pk')" },
            {"g.addV('person').property('id', 'mary').property('firstName', 'Mary').property('lastName', 'Andersen').property('age', 39).property('PartitionKey', 'pk')" },
            {"g.addV('person').property('id', 'ben').property('firstName', 'Ben').property('lastName', 'Miller').property('PartitionKey', 'pk')" },
            {"g.addV('person').property('id', 'robin').property('firstName', 'Robin').property('lastName', 'Wakefield').property('PartitionKey', 'pk')" },
            {"g.V('thomas').addE('knows').to(g.V('mary'))" },
            {"g.V('thomas').addE('knows').to(g.V('ben'))" },
            {"g.V('ben').addE('knows').to(g.V('robin'))" },
            {"g.V('thomas').property('age', 44)" },
            {"g.V().count()" },
            {"g.V().hasLabel('person').has('age', gt(40))" },
            {"g.V().hasLabel('person').values('firstName')" },
            {"g.V().hasLabel('person').order().by('firstName', decr)" },
            {"g.V('thomas').out('knows').hasLabel('person')" },
            {"g.V('thomas').out('knows').hasLabel('person').out('knows').hasLabel('person')" },
            {"g.V('thomas').repeat(out()).until(has('id', 'robin')).path()" },
            {"g.V('thomas').outE('knows').where(inV().has('id', 'mary')).drop()" },
            {"g.E().count()" },
        };

        public Task InsertTweet(Tweet tweet)
        {

            //var query = "Graph graph = TinkerFactory.createModern();";
            //var query2 = "graph.io(IoCore.graphml()).writeGraph(\"tinkerpop-modern.xml\");";
            //var res1 = ExecuteQuery(query).Result;
            //var res2 = ExecuteQuery(query2).Result;



            //var result = ExecuteQuery("g.io(\"result.xml\").write().iterate()");
            string text = EscapeChars(tweet.Text);
            string fullTextTweet = EscapeChars(tweet.FullText);
            string screenName = EscapeChars(tweet.User.ScreenName);

            //var antibioticsQueryTweet = $"g.V().has('topic', 'id', 'antibiotics').fold()" +
            //$".coalesce(unfold(), " +
            //$"g.addV('topic').property('id', 'antibiotics')";

            var baseTweetQuery =
                $"g.V().has('tweet', 'id', { tweet.Id.ToString() }).fold()" +
                $".coalesce(unfold(), " +
                $"g.addV('tweet').property(id, { tweet.Id.ToString() })" +
                $".property('createdAt', '{ tweet.CreatedAt.ToString() }')" +
                $".property('name', '{ text }') " +
                $".property('fullText', '{ fullTextTweet }')" +
                $".property('favoritedCount', { tweet.FavoriteCount.ToString() }))";

            var userQuery = $"g.V().has('user', 'id', { tweet.User.Id.ToString() }).fold()" +
                $".coalesce(unfold(), " +
                $"addV('user').property(id, { tweet.User.Id.ToString() })" +
                $".property('name', '{ screenName }'))";


            var userTweetEdgeQuery = $"g.V({ tweet.User.Id.ToString() })" +
                $".addE('tweeted').to(g.V({ tweet.Id.ToString() }))";

            //var userAntibioticEdgeQuery = $"g.V({ tweet.User.Id.ToString() })" +
            //$".addE('tweeted_about').to(g.V('antibiotics'))";

            var baseTweetQueryResult = ExecuteQuery(baseTweetQuery).Result;
            var userQueryResult = ExecuteQuery(userQuery).Result;
            var userTweetEdgeQueryResult = ExecuteQuery(userTweetEdgeQuery).Result;
            //var antibioticsQueryTweetResult = ExecuteQuery(antibioticsQueryTweet).Result;
            //var userAntibioticEdgeQueryResult = ExecuteQuery(userAntibioticEdgeQuery).Result;

            var topicQueries = GetTopicQueries(tweet);
            ExecuteQueries(topicQueries);

            if (tweet.QuotedStatusId.HasValue)
            {
                InsertTweet(tweet.QuotedStatus);
                var quotedEdgeQuery = $"g.V({ tweet.Id.ToString() })" +
                $".addE('quoted').to(g.V({ tweet.QuotedStatusId.ToString() }))";
                var quotedEdgeQueryResult = ExecuteQuery(quotedEdgeQuery).Result;
            }

            return Task.CompletedTask;
        }

        public async Task<ResultSet<dynamic>> ExecuteQuery(string query, int retry = 0)
        {
            try
            {
                //using (var gremlinClient = new GremlinClient(server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
                //{
                //    return await gremlinClient.SubmitAsync<dynamic>(query);
                //}

                using (var client = new GremlinClient(new GremlinServer("localhost", 8182)))
                {
                      return await client.SubmitAsync<dynamic>(query);
                }
            }
            catch (Exception e)
            {
                if (retry > 10)
                {
                    throw;
                }

                Thread.Sleep(1000);
                return await ExecuteQuery(query, retry + 1);
            }

        }

        private void ExecuteQueries(IEnumerable<string> queries)
        {
            foreach (string query in queries)
            {
                var queryResult = ExecuteQuery(query).Result;
            }
        }

        private IEnumerable<string> GetTopicQueries(Tweet tweet)
        {
            List<string> queries = new List<string>();

            foreach (string topic in TopicsToAnalyze())
            {
                var fullTextContains = tweet.FullText != null ? tweet.FullText.ToLower().Contains(topic.ToLower()) : false;
                var textContains = tweet.Text != null ? tweet.Text.ToLower().Contains(topic.ToLower()) : false;

                if (fullTextContains || textContains)
                {
                    queries.Add($"g.V().has('topic', 'id', '{ topic }').fold()" +
                    $".coalesce(unfold(), " +
                    $"g.addV('topic').property('id', '{ topic }')" +
                    $".property('pk', 'pk'))");

                    queries.Add($"g.V('{ tweet.User.Id.ToString() }')" +
                    $".addE('tweeted_about').to(g.V('{ topic }'))");
                }   
            }

            return queries;
        }

        private string EscapeChars(string escapeString)
        {
            string result = string.Empty;
            if (escapeString != null)
            {
                foreach (char c in escapeString)
                {
                    if (BadChars().Contains(c))
                    {
                        result += '\\';
                    }

                    result += c;
                }
            }

            return result;
        }

        private List<char> BadChars()
        {
            return new List<char>
            {
                '\'',
                '\n'
            };
        }

        private List<string> TopicsToAnalyze()
        {
            return new List<string>
            {
                "cancer",
                "coronavirus"
            };
        }
    }
}
