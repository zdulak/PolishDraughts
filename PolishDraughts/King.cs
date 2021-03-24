using System.Collections.Generic;
using System.Collections.ObjectModel;
using static System.Math;

namespace PolishDraughts
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
