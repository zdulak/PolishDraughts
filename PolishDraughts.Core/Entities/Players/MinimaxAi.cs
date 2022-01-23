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

        protected override (Position PiecePosition, Position TargetPosition) ChooseSimpleMove()
        {
            throw new System.NotImplementedException();
        }

        protected override Move ChooseCapturePath(List<Move> capturePaths)
        {
            throw new System.NotImplementedException();
        }
    }
}