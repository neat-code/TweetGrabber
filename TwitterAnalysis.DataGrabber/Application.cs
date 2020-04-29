namespace TwitterAnalysis.DataGrabber
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;
    using Tweetinvi;
    using Tweetinvi.Models;
    using TwitterAnalysis.Core.Adapters;
    using TwitterAnalysis.Core.Repositories;

    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {

        private readonly IConfiguration configuration;
        private readonly ITweetRepository tweetRepository;
        private readonly ITweetAdapter tweetAdapter;


        public Application(IConfiguration configuration, ITweetRepository tweetRepository, ITweetAdapter tweetAdapter)
        {
            this.configuration = configuration;
            this.tweetRepository = tweetRepository;
            this.tweetAdapter = tweetAdapter;
        }

        public void Run()
        {
            Auth.SetUserCredentials(configuration["TwitterApiKey"], configuration["ApiSecretKey"], configuration["AccessToken"], configuration["AccessTokenSecret"]);

            var stream = Stream.CreateFilteredStream();
            stream.AddTweetLanguageFilter(LanguageFilter.English);
            stream.AddTrack("Antibiotics");

            stream.StreamStopped += (sender, args) =>
            {
                stream.StartStreamMatchingAllConditions();
            };

            stream.MatchingTweetReceived += (sender, args) =>
            {
                var dbTweet = tweetAdapter.GetDbObject(args.Tweet, args.MatchingTracks);
                tweetRepository.InsertTweet(dbTweet).Wait();
                Console.WriteLine(args.Tweet);
            };
            stream.StartStreamMatchingAllConditions();
            Task.WaitAll();
        }
    }
}
