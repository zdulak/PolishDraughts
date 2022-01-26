using System;
using System.Collections.Generic;
using System.Threading;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class RandomAi : Computer
    {
        private readonly Random _random;

        public RandomAi(Color color, IBoard board, IView view) : base(color, board, view)
        {
            _random = new Random();
        }

        protected override Move GetComputerMove(List<Move> moves)
        {
            Thread.Sleep(1);
            return moves[_random.Next(moves.Count)];
        }
    }
}