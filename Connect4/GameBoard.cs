using System;

namespace Connect4
{
    public class GameBoard
    {
        const int CONNECTFOUR = 4;

        int _rows;
        int _columns;
        TokenColor[,] grid;
        int tokensCount;

        #region Constructor
        public GameBoard(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            grid = new TokenColor[_rows, _columns];
            tokensCount = 0;
            ClearGrid();
        }
        #endregion


        #region Methods
        /// <summary>
        /// mark all spots as blank
        /// </summary>
        private void ClearGrid()
        {
            for (int row = 0; row < _rows; row++)
                for (int column = 0; column < _columns; column++)
                    grid[row, column] = TokenColor.Blank;
        }

        /// <summary>
        /// display the status if the grid
        /// </summary>
        public void ShowGrid()
        {
            for (int y = _rows - 1; y >= 0; y--)
            {
                for (int x = 0; x < _columns; x++)
                {
                    Console.Write(grid[y, x].TokenColorString());
                    Console.Write(' ');
                }
                Console.Write('\n');
            }
        }

        /// <summary>
        /// change grid's status by placing a token in a specific column
        /// </summary>
        public void PlaceTokenInColumn(TokenColor playerColor, int column)
        {
            for (int y = 0; y < _rows; y++)
                if (grid[y, column] == TokenColor.Blank)
                {
                    grid[y, column] = playerColor;
                    break;
                }
            //update dropped tokens
            tokensCount++;
        }

        /// <summary>
        /// check if a column is full
        /// </summary>
        private bool CanPlaceTokenInColumn(int column)
        {
            //check if last row is blank
            if (grid[_rows - 1, column] != TokenColor.Blank)
                return false;
            return true;
        }

        /// <summary> check connect four in three directions horizontally, vertically, diagonally </summary>
        private bool CheckForWin(TokenColor playerColor) => horizontalCheck(playerColor) || verticalCheck(playerColor) || diagonalCheck(playerColor);


        /// <summary>
        /// check if there is connect four in all columns
        /// </summary>
        private bool verticalCheck(TokenColor playerColor)
        {
            // Vertical check:
            int upLimit = _rows - CONNECTFOUR;
            for (int y = 0; y <= upLimit; y++)
                for (int x = 0; x < _columns; x++)
                    if (grid[y, x] == playerColor && grid[y + 1, x] == playerColor
                        && grid[y + 2, x] == playerColor && grid[y + 3, x] == playerColor)
                        return true;
            return false;
        }

        /// <summary>
        /// check if there is connect four in all rows
        /// </summary>
        private bool horizontalCheck(TokenColor playerColor)
        {
            // Horizontal check:
            int rightLimit = _columns - CONNECTFOUR;
            for (int y = 0; y < _rows; y++)
                for (int x = 0; x <= rightLimit; x++)
                    if (grid[y, x] == playerColor && grid[y, x + 1] == playerColor
                       && grid[y, x + 2] == playerColor && grid[y, x + 3] == playerColor)
                        return true;
            return false;
        }


        /// <summary>
        /// diagonal check
        /// </summary>
        private bool diagonalCheck(TokenColor color)
        {
            int upLimit = _rows - CONNECTFOUR;
            for (int y = 0; y <= upLimit; y++)
                for (int x = 0; x < _columns; x++)
                    if (diagonalCheck(color, y, x))
                        return true;
            return false;

        }

        /// <summary>
        /// check for connect four in four directions down-left, up-right, down-right,up-left
        /// </summary>
        private bool diagonalCheck(TokenColor color, int positionX, int positionY)
        {
            var totalMatches = 1;

            // check down-left
            for (int counter = 1; counter < CONNECTFOUR; counter++)
            {
                var row = positionX - counter;
                var col = positionY - counter;

                // check grid limits
                if (!isWithinGrid(row, col))
                    break;

                if (grid[row, col] != color)
                    break;
                else
                {
                    totalMatches++;
                    if (totalMatches == CONNECTFOUR)
                        return true;
                }
            }

            // check up-right
            for (int counter = 1; counter < CONNECTFOUR; counter++)
            {
                var row = positionX + counter;
                var col = positionY + counter;

                // check grid limits
                if (!isWithinGrid(row, col))
                    break;

                if (grid[row, col] != color)
                    break;
                else
                {
                    totalMatches++;
                    if (totalMatches == CONNECTFOUR)
                        return true;
                }
            }

            //reset counter
            totalMatches = 1;

            //check down-right
            for (int counter = 1; counter < CONNECTFOUR; counter++)
            {
                var row = positionX - counter;
                var col = positionY + counter;

                // check grid limits
                if (!isWithinGrid(row, col))
                    break;

                if (grid[row, col] != color)
                    break;
                else
                {
                    totalMatches++;
                    if (totalMatches == CONNECTFOUR)
                        return true;
                }
            }

            // check up-left
            for (int counter = 1; counter < CONNECTFOUR; counter++)
            {
                var row = positionX + counter;
                var col = positionY - counter;

                // check grid limits
                if (!isWithinGrid(row, col))
                    break;

                if (grid[row, col] != color)
                    break;
                else
                {
                    totalMatches++;
                    if (totalMatches == CONNECTFOUR)
                        return true;
                }
            }

            return false;
        }

        private bool isWithinGrid(int column, int row) => column >= 0 && row >= 0 && column < _columns && row < _rows;
        private bool isWithinGrid(int column) => column >= 0 && column < _columns;

        private bool gridIsFull() => tokensCount >= _rows * _columns;


        /// <summary>
        /// check if the user's input is valid regarding the grid's status
        /// </summary>
        public bool ValidateUserInput(int column, out string ErrorMessage)
        {
            ErrorMessage = null;
            if (!isWithinGrid(column))
                ErrorMessage = $"\nThe integer must be between 1 and {_columns}.";
            else if (!CanPlaceTokenInColumn(column))
                ErrorMessage = "\nThat column is full.";
            var res = String.IsNullOrEmpty(ErrorMessage) ? true : false;
            return res;

        }

        /// <summary>
        /// game is over if there are no blank spots or someone has won
        /// </summary>
        public GameStatus CheckIfGameIsOver(TokenColor color)
        {
            if (CheckForWin(color))
                return GameStatus.Win;
            else if (gridIsFull())
                return GameStatus.Finished;
            else
                return GameStatus.NotCompleted;
        }

        #endregion

    }
}
