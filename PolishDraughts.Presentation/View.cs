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
        private readonly string _line = new string(' ', 3) + new string('-', 4 * IBoard.Size + 1);
        //A   B   C   etc.
        private readonly string _letters = string.Join(
            new string(' ', 3),
            Enumerable.Range(0, IBoard.Size).Select(n => (char) (n + 65)));

        public void Clear() => Console.Clear();

        public void DisplayMsg(string message) => Console.WriteLine(message);

        public void DisplayBoard(IBoard board)
        {
            Console.WriteLine();
            for (var row = 0; row < IBoard.Size; row++)
            {
                Console.WriteLine(_line);
                Console.Write((IBoard.Size - row).ToString().PadRight(3));
                for (var col = 0; col < IBoard.Size; col++)
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

        public void DisplayCapturePaths(List<Move> capturePaths)
        {
            Console.WriteLine("You have a mandatory capture. Choose one of the capture paths available below:");
            for (var i = 0; i < capturePaths.Count; i++)
            {
              Console.WriteLine($"{i + 1} {capturePaths[i]}");
            }
        }

        public void DisplayChoiceMenu(IEnumerable<string> options)
        {
            
            options = AddSpaceBeforeCapitalLetters(options);
            Console.WriteLine("Choose one options from the list below:");
            foreach (var (name, index) in options.Select((name, index) => (name, index + 1)))
            {
                Console.WriteLine($"{index}. {name}");
            }
        }

        public void DisplayMainMenu() => Console.WriteLine("Polish draughts game\n");

        public void DisplayRules() => Console.WriteLine(
            @"See: http://gambiter.com/checkers/International_draughts.html" + "\n");

        public void DisplayAbout() => Console.WriteLine("This game was made by Damian Zdulski\n");

        public void DisplayPlayerChoice(Color color) => Console.WriteLine(
            $"Please select type of player for {color.ToString().ToLower()} pieces or return to the main menu.");

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

        private IEnumerable<string> AddSpaceBeforeCapitalLetters(IEnumerable<string> sentences)
        {
            return sentences.Select(
                sentence => new string(
                    sentence.SelectMany(
                            (c, i) => i > 0 && char.IsUpper(c) ? new char[] { ' ', c } : new char[] { c })
                        .ToArray()));
        }
    }
}
