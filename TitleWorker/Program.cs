using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TitleWorker
{
    class Program
    {
        public static string Articles = "a,an,the";
        static void Main(string[] args)
        {
            FileProcess();
        }

        static void FileProcess()
        {
            StreamReader reader = new("Test.csv");
            StreamWriter writer = new("Complete.csv");
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                var tmpLine = ConvertLine(line);
                writer.WriteLine(ConvertLine(line));
            }
            reader.Close();
            writer.Close();
        }

        static string ConvertLine(string line)
        {
            StringBuilder sb = new();
            string[] words = line.Split(' ');
            words = DeleteArticles(words, out bool userCheck);
            
            for (int i = 0; i < words.Length; i++)
            {
                sb.Append(CapitalizeFirstLetter(words[i]));
                sb.Append(' ');
            }

            if (userCheck)
            {
                sb.Append(';');
                return sb.ToString() + "check";
            }
            else
            {
                return sb.ToString();
            }

        }

        static string[] DeleteArticles(string[] words, out bool userCheck)
        {
            List<string> wordsWithoutArticles = new();
            userCheck = false;
            foreach (var word in words)
            {
                if(!Articles.Contains(word))
                {
                    wordsWithoutArticles.Add(word);
                    if(word.Length <= 4)
                        userCheck = true;
                }
                
            }
            
            return wordsWithoutArticles.ToArray();

        }
        static string CapitalizeFirstLetter(string word)
        {
            var wordChar = word.ToCharArray();
            StringBuilder wordBuilder = new();
            wordBuilder.Append(wordChar[0].ToString().ToUpper());
            for (int i = 1; i < wordChar.Length; i++)
            {
                wordBuilder.Append(wordChar[i]);
            }
            return wordBuilder.ToString();
        }

    }
}
