using System.Collections.Generic;
using System.Collections.ObjectModel;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;

namespace PolishDraughts.Core.Entities.Pieces
{
    public abstract class Piece
    {
        public static ReadOnlyCollection<Position> MoveDirections { get; } = new List<Position>
        {
            new Position(1, 1), new Position(1, -1),
            new Position(-1, 1), new Position(-1, -1)
        }.AsReadOnly();

        public Color Color { get; set; }

        protected Piece(Color color) => Color = color;

        public void Capture() => Color = Color.None;

        public override string ToString() => GetType().Name;

        public abstract bool IsCorrectJump(Position piecePosition, Position targetPosition, bool isCapturing);
    }
}
