using System.Text;
using System.Text.RegularExpressions;

namespace WordFinder.App
{
    public class WordFinder
    {
        const int MaxMatrixSize = 64;
        const int NumberOfItemsToReturn = 10;
        const RegexOptions matchingRegexOptions = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

        private readonly List<string> _rows;
        private readonly List<string> _columns;

        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");

            var matrixSize = matrix.Count();

            if (matrixSize > MaxMatrixSize)
                throw new Exception($"The matrix exceeds the maximum size allowed. Max Size: {MaxMatrixSize}x{MaxMatrixSize}");

            _rows = matrix.ToList();
            _columns = new List<string>();

            for (int i = 0; i < matrixSize; i++)
            {
                var colBuilder = new StringBuilder(matrixSize);

                foreach (var row in matrix)
                {
                    colBuilder.Append(row[i]);
                }
                _columns.Add(colBuilder.ToString());
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (wordstream == null)
                throw new ArgumentNullException("wordstream");

            if (!wordstream.Any())
                return new List<string>();

            // remove duplicated and empty words and convert to Upper and Invariant
            var wordsToFind = wordstream
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .Select(word => word)
                .Distinct()
                .ToList();

            var searchResult = FindOccurrences(wordsToFind, _rows, _columns);

            return searchResult
                .OrderByDescending(x => x.Ocurrences)
                .Select(x => x.Word)
                .Take(NumberOfItemsToReturn);
        }

        private static List<FindWordResult> FindOccurrences(IEnumerable<string> wordsToFind, List<string> rows, List<string> columns)
        {
            var searchResult = new List<FindWordResult>();

            foreach (var word in wordsToFind)
            {
                var ocurrences = FindWordOccurrences(word, rows);
                ocurrences += FindWordOccurrences(word, columns);

                if (ocurrences == 0) continue;

                searchResult.Add(new FindWordResult { Word = word, Ocurrences = ocurrences });
            }

            return searchResult;
        }

        /// <summary>
        /// Searches all occurrences of the input string in the matrix
        /// </summary>
        private static int FindWordOccurrences(string word, List<string> list)
        {
            // for Right to Left comparison use the next regexOptions
            // RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.RightToLeft;

            return list.Select(p => Regex.Matches(p, word, matchingRegexOptions).Count).Sum();

            //var ocurrences = 0;

            //foreach (var row in list)
            //{
            //    ocurrences += Regex.Matches(row, word, matchingRegexOptions).Count;
            //}

            //return ocurrences;
        }
    }
}
