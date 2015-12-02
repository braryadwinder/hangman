using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Answer
    {
        public string Word { get; set; }
        public string[] Tips { get; set; }
        public int Size { get {
                return Word.Length;
            }
        }

        public Answer(string word, string[] tips)
        {
            this.Word = word;
            this.Tips = tips;
        }

        public Answer(string word)
        {
            this.Word = word.Trim();
            this.Tips = new string[10];
        }

        public override string ToString()
        {
            var tips = "[";
            foreach (string t in this.Tips)
            {
                if (t != null)
                    tips += t + ", ";
            }
            
            tips += "]";

            return "{" + this.Word + "," + tips + "}";
        }
    }
}
