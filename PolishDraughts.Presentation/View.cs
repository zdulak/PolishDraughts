using System;
using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Boards;
using PolishDraughts.Core.Entities.Pieces;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Presentation
{
    public class View : IView
    {
        private readonly string _line = new string(' ', 3) + new string('-', 4 * Board.Size + 1);
        //A   B   C   etc.
        private readonly string _letters = string.Join(
            new string(' ', 3),
            Enumerable.Range(0, Board.Size).Select(n => (char) (n + 65)));

        public void Clear() => Console.Clear();

        public void DisplayMsg(string message) => Console.WriteLine(message);

        public void DisplayBoard(IBoard board)
        {
            Console.WriteLine();
            for (var row = 0; row < Board.Size; row++)
            {
                Console.WriteLine(_line);
                Console.Write((Board.Size - row).ToString().PadRight(3));
                for (var col = 0; col < Board.Size; col++)
                {
                    Console.Write("|");
                    var position = new Position(row, col);
                    DisplaySlot(board[position], position);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(_line);
            Console.WriteLine(new string(' ', 5)+ _letters);
            Console.WriteLine();
        }

        public void DisplayCapturePaths(List<CapturePath> capturePaths)
        {
            DisplayMsg("You have a mandatory capture. Choose one of the capture paths available below:");
            for (var i = 0; i < capturePaths.Count; i++)
            {
                DisplayMsg($"{i + 1} {capturePaths[i]}");
            }
        }

        public void DisplayMainMenu()
        {
            throw new NotImplementedException();
        }

        public void DisplayRules()
        {
            throw new NotImplementedException();
        }

        public void DisplayAbout()
        {
            throw new NotImplementedException();
        }

        private void DisplaySlot(Piece piece, Position position)
        {
            switch (piece)
            {
                case null when position.IsValid():
                    Console.Write("///");
                    break;
                case null:
                    Console.Write("   ");
                    break;
                case Men when piece.Color == Color.White:
                    Console.Write("(W)");
                    break;
                case Men when piece.Color == Color.Black:
                    Console.Write("(B)");
                    break;
                case King when piece.Color == Color.White:
                    Console.Write("[W]");
                    break;
                case King when piece.Color == Color.Black:
                    Console.Write("[B]");
                    break;
                default:
                    Console.Write("Err");
                    break;
            }
        }
    }
}
