using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TwitterAnalysis.Core.Database;
using TwitterAnalysis.Core.Repositories;

namespace TwitterAnalysis.DataMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddAzureKeyVault("https://kv-twitteranalysis.vault.azure.net/")
                .Build();


            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IApplication, Application>()
                .AddSingleton<ITweetRepository, TweetRepository>()
                .AddSingleton<ITweetSqlDatabase, CosmosTweetSqlDatabase>()
                .AddSingleton<ITweetGraphDatabase, TweetGraphDatabase>()
                .BuildServiceProvider();

            serviceProvider.GetService<IApplication>().Run();
        }
    }
}
