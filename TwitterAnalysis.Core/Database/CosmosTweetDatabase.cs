namespace TwitterAnalysis.Core.Database
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TwitterAnalysis.Core.Models;
    using TwitterAnalysis.Core.Models.Tweet;

    public class CosmosTweetSqlDatabase : ITweetSqlDatabase
    {
        private readonly CosmosClient client;
        private readonly Database database;
        private readonly Container container;
        private readonly ITweetGraphDatabase tweetGraphDatabase;

        public CosmosTweetSqlDatabase(IConfiguration configuration, ITweetGraphDatabase tweetGraphDatabase)
        {
            client = new CosmosClient(configuration["CosmosConnection"]);
            database = client.GetDatabase("TwitterAnalysis");
            container = database.GetContainer("tweets");
            this.tweetGraphDatabase = tweetGraphDatabase;
        }

        public async Task<IEnumerable<TwitterSubject>> GetActiveSubjects()
        {
            List<TwitterSubject> twitterSubjects = new List<TwitterSubject>();
            var currentTime = DateTime.UtcNow;
            var q = container.GetItemLinqQueryable<TwitterSubject>();

            var items = q.Where(x =>
                x.DocumentType == DocumentType.TwitterSubject &&
                x.EndTime > currentTime &&
                x.StartDate <= currentTime &&
                x.PartitionKey == "TWITTER_SUBJECTS"
            );

            var iterator = items.ToFeedIterator();

            while (iterator.HasMoreResults)
            {
                foreach (var document in await iterator.ReadNextAsync())
                {
                    twitterSubjects.Add(document);
                }
            }

            return twitterSubjects;
        }

        public async Task InsertTweet(TweetDbObject tweetDbObject)
        {
            await container.CreateItemAsync(tweetDbObject);
        }

        public void CreateGraph(string collectionName)
        {

            QueryRequestOptions queryRequestOptions = new QueryRequestOptions
            {
                EnableScanInQuery = true,
            };

            var iterator = container.GetItemQueryIterator<TweetDbObject>("select * from c", null, queryRequestOptions);


            while (iterator.HasMoreResults)
            {
                var results = iterator.ReadNextAsync().Result;
                foreach (var document in results)
                {
                    var result = JsonConvert.DeserializeObject<TweetWrapper>(document.Tweet.ToString());
                    tweetGraphDatabase.InsertTweet(result.TweetDTO).Wait();
                    Console.WriteLine("Found one more batch of documents");
                }
            }
        }
    }
}