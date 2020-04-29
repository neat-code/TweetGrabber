namespace TwitterAnalysis.Core.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitterAnalysis.Core.Database;
    using TwitterAnalysis.Core.Models;

    public interface ITweetRepository
    {
        Task InsertTweet(TweetDbObject tweet);

        Task<IEnumerable<TwitterSubject>> GetActiveSubjects();
    }

    public class TweetRepository : ITweetRepository
    {
        private readonly ITweetSqlDatabase database;

        public TweetRepository(ITweetSqlDatabase database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<TwitterSubject>> GetActiveSubjects()
        {
            return await database.GetActiveSubjects();
        }

        public async Task InsertTweet(TweetDbObject tweet)
        {
            await database.InsertTweet(tweet);
        }
    }
}
