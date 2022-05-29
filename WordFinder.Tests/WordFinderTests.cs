using FluentAssertions;
using System.Collections.Generic;
using WordFinder.Tests.TestData;
using Xunit;

namespace WordFinder.Tests
{
    public class WordFinderTests
    {
        [Theory]
        [ClassData(typeof(WordFinderTestData))]
        public void WordFinder_FindTest(IEnumerable<string> matrix, IEnumerable<string> wordStream, IEnumerable<string> expectedResult)
        {
            //Act
            var result = new App.WordFinder(matrix).Find(wordStream);

            //Assert
            expectedResult.Should().BeEquivalentTo(result);
        }
    }
}
