using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenNLP.Tools.SentenceDetect;

namespace CCApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "Given an arbitrary text document written in English, write a program that will generate a concordance, i.e. an alphabetical list of all word occurrences, labeled with word frequencies. Bonus: label each word with the sentence numbers in which each occurrence appeared.";

            data.CreateSentenceDictionary(Parser.GetSentenceDetector())
                .CreateSentenceMap()
                .FormatMap()
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
