using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FeatureEngineering
{
    class ProcessInput
    {
        public static void ProcessInputTraining(Dictionary<String,bool> Stopwords,String inputFile,Dictionary<String,int> Features,bool RemoveStopWords,bool unigram,bool bigram,bool WordCount)
        {
            String line;
            Regex re = new Regex("[^0-9a-zA-Z-]");
            string fileName = Path.GetFileName(inputFile);
            using (StreamReader Sr = new StreamReader(inputFile))
            {
                while ((line = Sr.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(line))
                        continue;
                    if (RemoveStopWords)
                        line  = string.Join(" ",line.Split(' ').Select(w => Stopwords.ContainsKey(w) ? "" : w));
                    line = re.Replace(line, " ");
                    line = line.ToLower();
                    //if after removing all the unwanted stuff we have nothing then just pass over it
                    if (String.IsNullOrWhiteSpace(line))
                        continue;
                    string[] words = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (unigram)
                    {
                        foreach (var word in words)
                        {
                            if (Features.ContainsKey(word))
                                Features[word]++;
                            else
                                Features.Add(word, 1);
                        }
                    }
                    if(bigram)
                    {
                        string word;
                        for (int i = 1; i < words.Length; i++)
                        {
                            word = words[i - 1] + "_" + words[i];
                            if (Features.ContainsKey(word))
                                Features[word]++;
                            else
                                Features.Add(word, 1);
                        }
                    }
                    if(WordCount)
                    {
                        Features.Add("WordCount", words.Length);
                    }


                }
            }
        }
            
    }
}
