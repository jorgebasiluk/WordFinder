using System.Collections.Generic;
using Xunit;

namespace WordFinder.Tests.TestData
{
    public class WordFinderTestData : TheoryData<IEnumerable<string>, IEnumerable<string>, IEnumerable<string>>
    {
        public WordFinderTestData()
        {
            Add(InitializeMatrix(), new List<string> { "HOT", "COLD" }, new List<string> { "COLD" });
            Add(InitializeMatrix(), new List<string> { "SNOW", "COLD", "WIND", "CHILL" }, new List<string> { "SNOW", "WIND", "COLD", "CHILL" });
            Add(InitializeMatrix(), new List<string> { "SNOW", "COLD" }, new List<string> { "SNOW", "COLD" });
            Add(InitializeMatrix(), new List<string> { "HOT", "DRY" }, new List<string>());
            Add(InitializeMatrix(), new List<string> { "snow", "wind" }, new List<string> { "snow", "wind" });
        }

        private static IEnumerable<string> InitializeMatrix()
        {
            //CHILL X1
            //COLD X2
            //WIND X3
            //SNOW X4
            return new List<string>() {
                "XXCHILLXWIND",
                "CXXXXXXXXXXX",
                "OXXWINDXXXXX",
                "LCXXXXXXXXXX",
                "DOXXXSXXXXXX",
                "XLXXXNXXXXXS",
                "XDSXXOXXXXXN",
                "XXNXXWXXXXWO",
                "XXOXXXXXXXIW",
                "XXWXXXXXXXNX",
                "XXXSNOWXXXDX",
                "XXXXXXXXXXXX"
            };
        }
    }
}
