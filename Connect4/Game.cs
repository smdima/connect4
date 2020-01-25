using System;
using System.Collections.Generic;

namespace Connect4
{
    public class Game
    {
        #region Variables
        readonly GameBoard _gameBoard;
        readonly List<Player> _players;
        bool gameLoop = true;
        int index = 0;
        Player _currentPlayer => _players[index];
        #endregion

        #region Constructor
        public Game(GameBoard gameBoard, List<Player> players)
        {
            this._gameBoard = gameBoard;
            this._players = players;

            //init index to random player
            index = Utils.RandomNumber(0, _players.Count - 1);
        }
        #endregion

        #region Methods
        /// <summary>
        /// heart beat of the game
        /// </summary>
        public void Start()
        {
            while (gameLoop)
            {
                //execute players turn
                RenderGameConsole("");
                while (true)
                {
                    //read player input
                    int selectedColumn;
                    try { selectedColumn = _currentPlayer.Play(); }
                    catch (ArgumentException)
                    {
                        RenderGameConsole("Unable to parse as integer");
                        continue;
                    }

                    //check if drop is valid
                    if (!_gameBoard.ValidateUserInput(selectedColumn, out string ErrMsg))
                    {
                        RenderGameConsole(ErrMsg);
                        continue;
                    }

                    //drop token to the specific column
                    this._gameBoard.PlaceTokenInColumn(_currentPlayer.Color, selectedColumn);

                    //terminate the loop for asking user Input
                    break;
                }

                //check if game has been completed
                var status = this._gameBoard.CheckIfGameIsOver(_currentPlayer.Color);
                if (status == GameStatus.NotCompleted)
                {
                    //not completed, switch players
                    switchPlayers();
                }
                else
                {
                    //game completed
                    RenderGameConsole("");
                    Utils.PrintInformativeMessage(_currentPlayer, status);
                    gameLoop = false;
                }
            }
        }

        /// <summary>
        /// keeps player sequence
        /// </summary>
        private void switchPlayers()
        {
            index = (index + 1) % (_players.Count);
        }

        /// <summary>
        /// displays grid and informative messages
        /// </summary>
        private void RenderGameConsole(string msg)
        {
            System.Console.Clear();
            _gameBoard.ShowGrid();
            if (!String.IsNullOrEmpty(msg))
                Console.WriteLine(msg);
        }

        #endregion

    }
}
