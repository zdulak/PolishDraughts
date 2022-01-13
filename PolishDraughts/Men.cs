using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PolishDraughts
{
    public class Men : Piece
    {
        public Men(Color color) : base(color)
        {
        }

        public override bool IsCorrectJump(Position piecePosition, Position targetPosition, bool isCapturing)
        {
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

            return MoveDirections.Contains(targetPosition - piecePosition);
        }
    }
}
