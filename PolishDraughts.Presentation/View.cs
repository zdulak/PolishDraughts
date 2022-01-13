using System;
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
                case Men _ when piece.Color == Color.White:
                    Console.Write("(W)");
                    break;
                case Men _ when piece.Color == Color.Black:
                    Console.Write("(B)");
                    break;
                case King _ when piece.Color == Color.White:
                    Console.Write("[W]");
                    break;
                case King _ when piece.Color == Color.Black:
                    Console.Write("[B]");
                    break;
                default:
                    Console.Write("Err");
                    break;
            }
        }
    }
}
