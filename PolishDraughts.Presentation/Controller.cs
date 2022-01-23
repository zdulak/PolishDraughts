using System;
using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Presentation
{
    public class Controller : IController
    {
        public event Action QuitCommand;
        public IView View { get; }
        private const string QuitLiteral = "quit";

        public Controller(IView view)
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
                if (algebraicPosition?.ToLower() == QuitLiteral)
                {
                    OnQuitCommand();
                }
                if (IsInputValid(algebraicPosition))
                {
                    var position = new Position(algebraicPosition);
                    if (position.IsValid()) 
                        return position;

                    View.DisplayMsg("Invalid position");
                }
                else
                {
                    View.DisplayMsg("Invalid input.");
                }
            }
        }

        public Move GetPath(List<Move> capturePaths)
        {
            var number = GetOption(capturePaths.Count, () =>  View.DisplayCapturePaths(capturePaths));
            return capturePaths[number];
        }

        public int GetOption(int optionsNumber, Action messageView, bool clearScreen = false)
        {
            while (true)
            {
                messageView();
                var input = Console.ReadLine();
                if (input?.ToLower() == QuitLiteral)
                {
                    OnQuitCommand();
                }
                if (int.TryParse(input, out var choice) && choice >= 1 && choice <= optionsNumber)
                    return --choice;

                if (clearScreen)
                    View.Clear();
                View.DisplayMsg("Invalid input.\n");
            }
        }

        public void GetExitKey()
        {
            Console.WriteLine("Press any key to return to main menu.");
            Console.Read();
        }

        public void Quit() => Environment.Exit(0);

        private bool IsInputValid(string input)
        {
            return char.IsLetter(input[0]) &&
                   ((input.Length == 2 && char.IsDigit(input[1])) ||
                    (input.Length == 3 && input[1..] == "10"));
        }

        private void OnQuitCommand() => QuitCommand?.Invoke();
    }
}
