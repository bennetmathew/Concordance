using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Concordance
{
    public class ConcordanceHelper
    {
        // This method is used to remove special charactes and trim end of the sentence
        public string SanitizeSentence(string sentence)
        {
            sentence = sentence.TrimEnd(new Char[] { '!', '?', '.' }).ToLower();
            return Regex.Replace(sentence, "[,:?!;()]", "");
        }

        // Separates sentences from input text and assigns sentence number
        public List<Sentence> GetSentences(string input)
        {
            string rmvBreaks = Regex.Replace(input, @"\r\n?|\n", "");
            int index = 1;
            return Regex.Split(rmvBreaks, @"(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?)\s")
                        .Select(x => new Sentence
                        {
                            Item = x,
                            Index = index++
                        }).ToList();
        }

        // Checks whether a matchng word exists inside the list object
        public int MatchWord(List<Concordance> concordList, string inputWord)
        {
            int index = -1;
            foreach (Concordance item in concordList)
            {
                if(item.Word == inputWord)
                {
                    index = concordList.IndexOf(item);
                    break;
                }
            }
            return index;
        }

        // Main method to generate concordance
        public List<Concordance> GenerateConcordance(string input)
        {
            List<Sentence> sentencesList = GetSentences(input);
            List<Concordance> concordList = new List<Concordance>();

            foreach (Sentence sentence in sentencesList)
            {
                sentence.Item = SanitizeSentence(sentence.Item);

                foreach (string word in sentence.Item.Split(' '))
                {
                    int findIndex = MatchWord(concordList, word);
                    if (findIndex == -1) // if the word does not exist
                    {
                        Concordance concordObj = new Concordance();
                        concordObj.Word = word;
                        concordObj.Frequency = 1;
                        concordObj.IndexList = new List<int>(new int[] { sentence.Index });
                        concordList.Add(concordObj);
                    }
                    else // if the word already exist
                    {
                        concordList[findIndex].Frequency++;
                        concordList[findIndex].IndexList.Add(sentence.Index);
                    }
                }
            }

            return concordList;
        }

        // Prints the desired output
        public void DisplayOutput(List<Concordance> concordList)
        {
            concordList = concordList.OrderBy(x => x.Word).ToList();
            char i = 'a', r ='a';
            foreach (Concordance concord in concordList)
            {
                string index = ((char.IsLetter(i)) ? i.ToString() : string.Format("{0}{0}", (r++).ToString()).ToString());
                Console.WriteLine(string.Format("{0}. {1} {{{2}: {3}}}", index, concord.Word, concord.Frequency, string.Join(", ", concord.IndexList)));
                i++;
            }
        }
    }
}
