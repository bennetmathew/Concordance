using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Concordance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input = "Given an arbitrary text document written in English, write a program that will generate a concordance, i.e. an alphabetical list of all word occurrences, labeled with word frequencies. Bonus: label each word with the sentence numbers in which each occurrence appeared.";

            ConcordanceHelper concordHelperObj = new ConcordanceHelper();
            
            concordHelperObj.DisplayOutput(concordHelperObj.GenerateConcordance(input));
        }
    }
}
