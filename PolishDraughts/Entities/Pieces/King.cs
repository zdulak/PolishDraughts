using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;

namespace PolishDraughts.Core.Entities.Pieces
{
    public class King : Piece
    {
        public King(Color color) : base(color)
        {
        }

        public override bool IsCorrectJump(Position piecePosition, Position targetPosition, bool isCapturing) =>
            MoveDirections.Contains((targetPosition - piecePosition).Normalize());
    }
}
