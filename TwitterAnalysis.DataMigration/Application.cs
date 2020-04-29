namespace TwitterAnalysis.DataMigration
{
    using System;
    using TwitterAnalysis.Core.Database;

    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private ITweetSqlDatabase tweetDatabase;

        public Application(ITweetSqlDatabase tweetDatabase)
        {
            this.tweetDatabase = tweetDatabase;
        }

        public void Run()
        {
            tweetDatabase.CreateGraph("dummy");
        }
    }
}
