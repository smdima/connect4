using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Player
    {
        public string Id;
        public TokenColor Color;

        public int Play()
        {
            //read input
            Console.WriteLine("");
            Console.Write($"Player {Id} ({Color}) : ");
            var userInput = Console.ReadLine();

            //parse
            int column = -1;
            if (!Int32.TryParse(userInput, out column))
                throw new ArgumentException();

            //make column zero-based
            column--;

            //done
            return column;
        }
    }
}
