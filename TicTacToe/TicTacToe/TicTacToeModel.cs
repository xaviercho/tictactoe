using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiBit.Net
{
    public class TicTacToeModel
    {
        private int[,] _tttGrid = new int[3, 3];
        private int _moveCount = 0;
        private char _winner = ' ';

        public TicTacToeModel()
        {
            Reset();
        }

        public void Reset()
        {
            _moveCount = 0;
            _winner = ' ';
            for (int i = 0; i < _tttGrid.GetLength(0); i++)
                for (int j = 0; j < _tttGrid.GetLength(1); j++)
                {
                    _tttGrid[i, j] = 32;
                }
        }

        public char WhoseTurn()
        {
            if (_moveCount % 2 == 1)
                return 'O';
            return 'X';
        }

        public char GetValue(int Column, int Row)
        {
            if ((Column > 2 || Column < 0) || (Row > 2 || Row < 0))
                throw new Exception("Cell is out of range");
            switch (_tttGrid[Row, Column])
            {
                case 88: return 'X';
                case 79: return 'O';
                default: return ' ';
            }
        }

        public void MakeMove(int Column, int Row)
        {
            if (WhoIsWinner() != ' ')
                throw new Exception("Game Over");
            else if (GetValue(Column, Row) != ' ')
                throw new Exception("Cell is already valued");

            _tttGrid[Row, Column] = WhoseTurn();
            _moveCount++;
            WhoIsWinner();
        }

        public char WhoIsWinner()
        {
            if (_winner != ' ') return _winner;

            if ((_tttGrid[0, 0] == _tttGrid[1, 1] &&
                  _tttGrid[0, 0] == _tttGrid[2, 2] && _tttGrid[0, 0] != ' ') ||
                  (_tttGrid[2, 0] == _tttGrid[1, 1] &&
                  _tttGrid[2, 0] == _tttGrid[0, 2] && _tttGrid[2, 0] != ' '))
            {
                _winner = (char)_tttGrid[1, 1];
                return _winner;
            }


            for (int i = 0; i < _tttGrid.GetLength(0); i++)
            {
                if (_tttGrid[i, 0] == _tttGrid[i, 1] &&
                    _tttGrid[i, 0] == _tttGrid[i, 2] && _tttGrid[i, 0] != ' ')
                {
                    _winner = (char)_tttGrid[i, 0];
                    return _winner;
                }
                else if (_tttGrid[0, i] == _tttGrid[1, i] &&
                    _tttGrid[0, i] == _tttGrid[2, i] && _tttGrid[0, i] != ' ')
                {
                    _winner = (char)_tttGrid[0, i];
                    return _winner;
                }
            }

            for (int i = 0; i < _tttGrid.GetLength(0); i++)
                for (int j = 0; j < _tttGrid.GetLength(1); j++)
                {
                    if (_tttGrid[i, j] == ' ')
                        return _winner;
                }

            return 'D';
        }
    }
}
