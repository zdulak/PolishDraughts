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

        protected override (Position piecePosition, Position targetPosition) ChooseMove()
        {
            throw new System.NotImplementedException();
        }

        protected override CapturePath ChooseCapture(List<CapturePath> capturePaths)
        {
            throw new System.NotImplementedException();
        }
    }
}