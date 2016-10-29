using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenNLP.Tools.SentenceDetect;

namespace CCApp
{
    public static class Parser
    {
        public static EnglishMaximumEntropySentenceDetector GetSentenceDetector()
        {
            var modelPath = "Resources\\EnglishSD.nbin";
            return new EnglishMaximumEntropySentenceDetector(modelPath);
        }

        public static Dictionary<int, string> CreateSentenceDictionary(this string pData, EnglishMaximumEntropySentenceDetector pDetector)
        {
            int index = 1;
            return pDetector.SentenceDetect(pData).ToDictionary(p => index++);
        }

        public static IEnumerable<Tuple<int, string>> CreateSentenceMap(this Dictionary<int, string> pDictionary)
        {
            var trim = new Regex("(^[\\W_]*)|([\\W_]*$)");

            return (from sentence in pDictionary
                    from word in sentence.Value.Split(' ')
                    select new Tuple<int, string>(sentence.Key, trim.Replace(word, "")));
        }

        public static IEnumerable<string> FormatMap(this IEnumerable<Tuple<int, string>> pWords) {
            int nextId = 1;
            return (from grouping in pWords
                .OrderBy(pO => pO.Item2)
                .GroupBy(p => p.Item2)
                    let lineIds = grouping.Select(tuple => tuple.Item1)
                    let next = nextId++
            select $"{next}. {grouping.Key} {{{grouping.Count()}:{string.Join(",", lineIds)}}}");
        }
    }
}
