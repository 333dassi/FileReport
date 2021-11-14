using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;


namespace FileDataReport
{
    class Read
    {

        const string FILENAME = "dickens.txt";
        const string FILPATH = "C:/Users/דסי/Desktop/Hadasim_homeTest/" + FILENAME;
        int countLines, countWords, countUniqueWords , commonWord = 0;
        string theCommonWord = "";
        string s = "";
        string[] words = { };
        Dictionary<string, int> Unique_words = new Dictionary<string, int>();
        int[] sentenceLengths = new int[3000];
        int WordsPersentCounter, WordsPerLineCounter, WordsNextsentence, maxSentence = 0;
        double avgSentence;
    
        public void Reporting()
        {
            try//reading from the file
            {
                using (StreamReader sr = File.OpenText(FILPATH))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        CountNumOfLines();//countUniqueWords++;
                        CutWordes();//cut the words from Punctuation and put them into words array
                        FillUniqueWordes();
                        CountWordsPerSentence();
                    }
                    Console.WriteLine("");
                    sr.Close();
                }
            }
            catch (Exception MyExcep)
            {
                Console.WriteLine(MyExcep.ToString());
            } 
            // אבל בגלל שיש לי 3 דברים בתוך הפוראיטש היה עדיף לעשות פוראיטש מאשר 3 פעמים לשלוף. יכולתי להמנע מהפואיטש
            // insted of CountNumOfUniqueWords:
            //Unique_words.Count(w => w.Value == 1);
            foreach (KeyValuePair<string, int> u in Unique_words)//run over the dictionery
            {
                CountNUmOfWords(u);
                CountNumOfUniqueWords(u);
                CountNumOfCommonWords(u);
            }
            MaxAndAvgSent();
            printReport();
        }
        private void printReport()
        {
            Console.Write(" The number of lines in  the file {0} is : {1} \n\n", FILENAME, countLines);
            Console.Write(" The number of words in  the file {0} is : {1} \n\n", FILENAME, countWords);
            Console.Write(" The number of the unique words in  the file {0} is : {1} \n\n", FILENAME, countUniqueWords);
            Console.Write(" The maximum sentence length in a file is:  {0}   \n\n" ,maxSentence);
            Console.Write(" The average sentence length in a file is:  {0}   \n\n", avgSentence);
            Console.Write(" The  common word in  the file {0} is : {1} \n\n", FILENAME, theCommonWord);
            Console.Write(" There are colors in the file  {0}  : \n\n", FILENAME); CountColors();
        }
        private void CountColors()
        {
            var colors = new[] { "red", "orange", "yellow", "green", "blue", "cyan", "purple", "white", "black", "brown", "magenta", "tan", "navy", "silver", "indigo", "violet", "pink", "gray" };

            foreach (KeyValuePair<string, int> u in Unique_words) //run over the dictionery- if threr is a unique
            {
                if (colors.Contains(u.Key))
                {
                    Console.Write(" the color {0} is appears {1} times.\n\n", u.Key, u.Value);
                }
            }
        }
        private void FillUniqueWordes()
        {
            foreach (string word in words)//cheking how many instances per word
            {
                Unique_words.TryGetValue(word, out int instances);
                Unique_words[word] = instances + 1;
            }
        }
        private void CutWordes()
        {
            string[] Punctuation = { " ", ",", "?", "!", ".", ":", ";", "'", "\"\"", "-", "\"" };
            s = s.Trim();//cut the unnessery spaces
            words = s.ToLower().Split(Punctuation, StringSplitOptions.RemoveEmptyEntries);//cut the words from Punctuation 
        }
        private void CountNumOfLines()
        {
            countLines++;
        }
        private void CountNUmOfWords(KeyValuePair<string, int> u)
        {
            
            countWords = countWords + u.Value;
        }
        private void CountNumOfUniqueWords(KeyValuePair<string, int> u)
        {
            if (u.Value == 1) //overing the words array and chek- if there is an exist word it Increases the counter if not- make a couter
            {
                countUniqueWords++;
            }
        }
        private void CountNumOfCommonWords(KeyValuePair<string, int> u)
        {
            var linkWords = new[] { "a", "about", "above", "after", "again", "against", "ain", "all", "am", "an", "and", "any", "are", "aren", "aren't", "as", "at", "be", "because", "been", "before", "being", "below", "between", "both", "but", "by", "can", "couldn", "couldn't", "d", "did", "didn", "didn't", "do", "does", "doesn", "doesn't", "doing", "don", "don't", "down", "during", "each", "few", "for", "from", "further", "had", "hadn", "hadn't", "has", "hasn", "hasn't", "have", "haven", "haven't", "having", "he", "her", "here", "hers", "herself", "him", "himself", "his", "how", "i", "if", "in", "into", "is", "isn", "isn't", "it", "it's", "its", "itself", "just", "ll", "m", "ma", "me", "mightn", "mightn't", "more", "most", "mustn", "mustn't", "my", "myself", "needn", "needn't", "no", "nor", "not", "now", "o", "of", "off", "on", "once", "only", "or", "other", "our", "ours", "ourselves", "out", "over", "own", "re", "s", "same", "shan", "shan't", "she", "she's", "should", "should've", "shouldn", "shouldn't", "so", "some", "such", "t", "than", "that", "that'll", "the", "their", "theirs", "them", "themselves", "then", "there", "these", "they", "this", "those", "through", "to", "too", "under", "until", "up", "ve", "very", "was", "wasn", "wasn't", "we", "were", "weren", "weren't", "what", "when", "where", "which", "while", "who", "whom", "why", "will", "with", "won", "won't", "wouldn", "wouldn't", "y", "you", "you'd", "you'll", "you're", "you've", "your", "yours", "yourself", "yourselves", "could", "he'd", "he'll", "he's", "here's", "how's", "i'd", "i'll", "i'm", "i've", "let's", "ought", "she'd", "she'll", "that's", "there's", "they'd", "they'll", "they're", "they've", "we'd", "we'll", "we're", "we've", "what's", "when's", "where's", "who's", "why's", "would" };
            bool doesItContain = linkWords.Contains(u.Key);
            if (u.Value > commonWord && !doesItContain)
            {
                commonWord = u.Value;
                theCommonWord = u.Key;
            }

        }
        private void CountWordsPerSentence()
        {
            if (s.Length != 0) //cheking if this is not a blank line.
            {
                int difference = 0;
                string[] WordsUpPoint = s.Split("."); //cut the words up to a point
                WordsPersentCounter = WordsUpPoint[0].Trim().Split(" ").Count();
                WordsPerLineCounter = s.Trim().Split(" ").Count(); //all the line`s words
                difference = WordsPerLineCounter - WordsPersentCounter;
                if (WordsNextsentence != 0) //cheking if there are  words from the previous line
                {
                    WordsPersentCounter = WordsPersentCounter + WordsNextsentence;

                }
                if (difference > 0) //cheking if there are words in the current line, after the point 
                {
                    WordsNextsentence = difference;
                }
                if (s.Contains("."))
                {
                    sentenceLengths[WordsPersentCounter]++; //get into an array of counters
                    if (difference == 0) //if there is no words after the line & there is point in this line- means that we alredy used the WordsNextsentence 
                    {
                        WordsNextsentence = 0;
                    }
                }
                else
                {
                    WordsNextsentence = WordsPersentCounter; // if there is no point in the line - count the current line`s words to the next sentence  
                }

            }
        }
        private void MaxAndAvgSent()
        {
            int i;
            int sum = 0;
            int numOfSent = 0;
            for(i= sentenceLengths.Length - 1; i >= 0; i--)
            {
                if (sentenceLengths[i] != 0) // there is a sentence with legth = i
                {
                    sum += sentenceLengths[i] * i;//cont the all sentences
                    numOfSent += sentenceLengths[i];// count num of sentences
                    if(i> maxSentence)// will happend jast one time -in the  first left index that not contains 0
                    {
                        maxSentence = i;

                    }
                }
            }
            if (numOfSent != 0)
            {
                avgSentence = sum / numOfSent;
            }
        }

    }
    
}

