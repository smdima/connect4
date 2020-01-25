using System;
using System.Collections.Generic;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup players
            var players = new List<Player>()
            {
                new Player()
                {
                    Id="Green",
                    Color= TokenColor.Green
                },
                new Player()
                {
                    Id="Red",
                    Color=TokenColor.Red
                }
                };

            //setup board
            GameBoard gameBoard = new GameBoard(6, 7);

            //setup game
            var game = new Game(gameBoard, players);

            //start game
            game.Start();

            //finish
            Console.ReadKey();
        }
    }
}
