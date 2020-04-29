namespace TwitterAnalysis.Core.Models.Tweet
{
    using System.Collections.Generic;

    public class Hashtag
    {
        public string Text { get; set; }

        public IEnumerable<int> Indices { get; set; }
    }
}
