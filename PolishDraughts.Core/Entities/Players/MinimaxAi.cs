using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class MinimaxAi : Computer
    {
        public MinimaxAi(Color color, IBoard board, IView view) : base(color, board, view)
        {
        }

        protected override Move GetComputerMove(List<Move> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}