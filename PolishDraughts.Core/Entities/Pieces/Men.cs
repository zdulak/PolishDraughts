using System.Linq;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;

namespace PolishDraughts.Core.Entities.Pieces
{
    public class Men : Piece
    {
        public Men(Color color) : base(color)
        {
        }

        public override bool IsCorrectJump(Position piecePosition, Position targetPosition, bool isCapturing)
        {
            var jump = targetPosition - piecePosition;
            if (!isCapturing)
            {
                switch (Color)
                {
                    case Color.Black:
                        return MoveDirections.ToArray()[..2].Contains(targetPosition - piecePosition);
                    case Color.White:
                        return MoveDirections.ToArray()[2..].Contains(targetPosition - piecePosition);
                }
            }

            return MoveDirections.Contains(jump);
        }
    }
}
