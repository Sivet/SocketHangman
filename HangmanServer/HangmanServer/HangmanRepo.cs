using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanServer
{
    class HangmanRepo
    {
        private static List<string> WordList;
        private Random random = new Random();
        public HangmanRepo()
        {
            if (WordList == null)
            {
                WordList = new List<string>();
                FillList();
            }
        }
        private void FillList()
        {
            WordList.Add("Tissemand");
            WordList.Add("Abekat");
            WordList.Add("Mountaindew");
            WordList.Add("Handsker");
            WordList.Add("Whiteboardmaker");
            WordList.Add("Computer");
        }
        public List<string> GetWordList()
        {
            return WordList;
        }
        public void AddNewWord(string word)
        {
            WordList.Add(word);
        }
        public void DeleteWord(string word)
        {
            //Not implemented yet
            //Check if you'd have to rearange the list, or if removing an object will create empty spots in the list
            //Empty spots would give the concern of having no word to guess
        }
        public string GetRandomWord()
        {
            string word;
            word = WordList[random.Next(0, WordList.Count)];
            return word;
        }
    }
}
