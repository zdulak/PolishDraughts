using System;
using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;

namespace PolishDraughts.Presentation
{
    public class Controller
    {
        public View View { get; }

        public Controller(View view)
        {
            View = view;
        }

        public Position GetPosition(int kind)
        {
            var msg = kind switch
            {
                1 => "Enter a position of a piece in the algebraic notation, i.e., a1, b4, etc.",
                2 => "Enter a target position of the piece in the algebraic notation, i.e., a1, b4, etc.",
                _ => throw new ArgumentOutOfRangeException()
            };
            View.DisplayMsg(msg);

            while (true)
            {
                var algebraicPosition = Console.ReadLine();
                if (IsInputValid(algebraicPosition))
                {
                    var position = new Position(algebraicPosition);
                    if (position.IsValid()) return position;

                    View.DisplayMsg("Invalid position");
                }
                else
                {
                    View.DisplayMsg("Invalid input.");
                }
            }
        }

        public CapturePath GetPath(List<CapturePath> capturePaths)
        {
            View.DisplayMsg("You have a mandatory capture. Choose one of the capture paths available below:");
            for (var i = 0; i < capturePaths.Count; i++)
            {
                View.DisplayMsg($"{i+1} {capturePaths[i]}");
            }

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var index))
                {
                    index -= 1;
                    if (index >= 0 && index < capturePaths.Count) return capturePaths[index];
                }

                View.DisplayMsg("Invalid input.");
            }
        }

        private bool IsInputValid(string input)
        {
            return char.IsLetter(input[0]) &&
                   ((input.Length == 2 && char.IsDigit(input[1])) ||
                    (input.Length == 3 && input[1..] == "10"));
        }
    }
}
