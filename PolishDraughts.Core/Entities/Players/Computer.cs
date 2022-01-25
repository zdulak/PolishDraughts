using System;
using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class Computer : Player
    {
        private readonly IView _view;
        private readonly Random _random;
        public Computer(Color color, IBoard board, IView view) : base(color, board)
        {
            _view = view;
            _random = new Random();
        }

        protected override Move ChooseMove(List<Move> moves)
        {
            var move = GetPlayerChoiceFromList(moves);
            if (move.CapturedPieces == null)
            {
                _view.DisplayMsg("Computer makes a move: " + $"{move.Path.First()}->{move.Path.Last()}");
            }
            else
            {
                _view.DisplayMsg($"Computer makes a mandatory capture: {move}");
            }

            return move;
        }

        private T GetPlayerChoiceFromList<T>(IReadOnlyList<T> elements)
        {
            System.Threading.Thread.Sleep(300);
            return elements[_random.Next(elements.Count)];
        }
    }
}
