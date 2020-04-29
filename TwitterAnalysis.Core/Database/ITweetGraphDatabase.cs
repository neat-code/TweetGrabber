namespace TwitterAnalysis.Core.Database
{
    using System.Threading.Tasks;
    using TwitterAnalysis.Core.Models.Tweet;

    public interface ITweetGraphDatabase
    {
        Task InsertTweet(Tweet tweet);
    }
}