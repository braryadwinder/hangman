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
        private int NextTip;
        public bool End { get; set; }
        private int errors = 6;
        private List<char> WrongGuesses;


        public Game(string path)
        {
            StreamReader file;

            if (!System.IO.File.Exists(path))
            {
                new System.Exception("Inexistent path");
            }
            else
            {
                if (!File.Exists(path))
                {
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
                this.NextTip = 1;
                this.WrongGuesses = new List<char>();
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
            Console.Clear();
            Console.WriteLine(this.CorrectLetters.Aggregate(string.Empty, (c, i) => c + i + ' ') + '\n');
            
            for (var i = 0; i < this.usedTips.Count; i++)
            {
                    Console.WriteLine( "Dica: " + this.usedTips[i].Trim());
            }

            Console.WriteLine();
            var tips =
                from t in this.Answer.Tips
                where t != null
                select t;

            if (tips.Count() != this.usedTips.Count)
            {
                Console.WriteLine("[F2] Nova dica [letra] Palpite | Erros : {0}", this.WrongGuesses.Count());
                return;
            }

            Console.WriteLine("[letra] Palpite | Erros : {0}", this.WrongGuesses.Count());            
        }

        public void checkGuess(char guess){
            if (this.Answer.Word.ToLower().Contains(guess.ToString().ToLower()))
            {
                var correct = this.CorrectLetters.ToCharArray();
                for (int i = 0; i < this.Answer.Size; i++)
                {
                    if(this.Answer.Word.ToLower()[i] == guess.ToString().ToLower()[0]){
                        correct[i] = guess;
                    }
                }
                    

                this.CorrectLetters = new string(correct);

                if (this.CorrectLetters.ToString().ToLower() == this.Answer.Word.ToLower())
                {
                    this.End = true;
                }
            }
            else
            {
                this.errors--;
                
                if (this.errors <= 0)
                {
                    this.End = true;
                }

                this.WrongGuesses.Add(guess);
            }
        }

        public void showNewTip()
        {
            if (Answer.Tips.Count() > 0 && Answer.Tips[this.NextTip] != null)
            { 
                this.usedTips.Add(Answer.Tips[this.NextTip]);
                this.NextTip++;
            }
        }

        public override string ToString()
        {
            return this.Answer.ToString();
        }
    }
}
