using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(args[0] != null ? args[0] : "jogo.txt");

            game.renderBoard();
            while( !game.end ){
                game.checkGuess((char) Console.Read());
                game.renderBoard();
            }
        }
    }
}
