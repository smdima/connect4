using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public static class Utils
    {
        
        /// <summary>
        /// static extensin for display
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string TokenColorString(this TokenColor token)
        {
            switch (token)
            {
                case TokenColor.Blank:
                    return "|";
                case TokenColor.Green:
                    return "G";
                case TokenColor.Red:
                    return "R";
                default:
                    return null;
            }
        }
        //----------------------------------------------------------------------------------
        /// <summary>
        /// get a random number within the range[min, max]
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameStatus"></param>
        internal static void PrintInformativeMessage(Player player, GameStatus gameStatus)
        {
            if (gameStatus == GameStatus.Win)
                Console.Write($"\nPlayer {player.Id}  has won!\n");
            else if (gameStatus == GameStatus.Finished)
                Console.Write("Sorry, no winner, Game is over");
        }
        

    }
}
