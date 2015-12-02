using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Main game class
/// </summary>

namespace Hangman
{
    public class Game
    {
        private Answer Answer { get; set; }
        private List<string> usedTips { get; set; }
        private string CorrectLetters;
        private int nextTip;

        public Game(string path)
        {
            StreamReader file;

            if (!System.IO.File.Exists(path))
            {
                new System.Exception("Inexistent path");
            }
            else
            {
                if (!File.Exists(path)) { 
                    new Exception("fudeu");
                }
                
                file = new StreamReader(path);
                var words = Game.findAnswer(file);

                if (words.Count < 1)
                    new Exception("nao ha palavras nesse arquivo");

                this.Answer = this.chooseRandomWord(words);
                this.CorrectLetters = new string('_', this.Answer.Size);
                this.usedTips = new List<string>();
                this.usedTips.Add(this.Answer.Tips[0]);
                this.nextTip = 1;
            }


        }

        static private List<Answer> findAnswer(StreamReader file)
        {
            string line = "";
            List<Answer> answers = new List<Answer>();
            Answer ans = null;
            int countTips = 0;

            for (var i = 0; i < 100 && (line = file.ReadLine()) != null; i++)
            {
                if (line[0] == 'P')
                {
                    ans = new Answer(line.Substring(2));
                    answers.Add(ans);
                    countTips = 0;
                }
                else if (line[0] == 'D')
                {
                    ans.Tips[countTips] = line.Substring(2);
                    countTips++;
                }
            }

            return answers;
        }

        private Answer chooseRandomWord(List<Answer> words){
            var choosen = words[Utils.Random.Next(0, words.Count)];

            return choosen;
        }

        public void renderBoard()
        {
            Console.WriteLine(this.CorrectLetters.Replace("_", "_ "));
            
            for (var i = 0; i < this.usedTips.Count; i++)
            {
                Console.WriteLine( this.usedTips[i].Trim() );
            }
            Console.WriteLine("[F2] Nova dica [letra] Palpite");            
        }

        public void checkGuess(char guess){
            if (this.Answer.Word.Contains(guess))
            {
                var index = this.Answer.Word.IndexOf(guess);
                var correct = this.CorrectLetters.ToCharArray();
                correct[index] = guess;

                this.CorrectLetters = new string(correct);
            }
        }

        public override string ToString()
        {
            return this.Answer.ToString();
        }


    }
}
