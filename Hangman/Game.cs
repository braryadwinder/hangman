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
        public Answer Answer { get; set; }

        public Game() { }

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

        public override string ToString()
        {
            return this.Answer.ToString();
        }
    }
}
