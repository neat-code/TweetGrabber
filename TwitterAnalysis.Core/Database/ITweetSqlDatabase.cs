namespace TwitterAnalysis.Core.Database
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitterAnalysis.Core.Models;

    public interface ITweetSqlDatabase
    {
        Task<IEnumerable<TwitterSubject>> GetActiveSubjects();

        Task InsertTweet(TweetDbObject tweetDbObject);

        void CreateGraph(string collectionName);
    }
}