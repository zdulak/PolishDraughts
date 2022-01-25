using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class MinimaxAi : Player
    {
        public MinimaxAi(Color color, IBoard board) : base(color, board)
        {
        }

        protected override Move ChooseMove(List<Move> moves)
        {
            throw new System.NotImplementedException();
        }
    }
}