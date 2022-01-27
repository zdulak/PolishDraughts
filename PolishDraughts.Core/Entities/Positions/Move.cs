using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PolishDraughts.Core.Entities.Pieces;

namespace PolishDraughts.Core.Entities.Positions
{
    public record Move(ReadOnlyCollection<Position> Path, bool Crowned = false, ReadOnlyCollection<Position> CapturedPositions = null,
        ReadOnlyCollection<Piece> CapturedPieces = null)
    {
        public override string ToString()
        {
            var path = string.Join("->", Path);
            var captured = CapturedPositions.Zip(
                CapturedPieces,
                (position, piece) => $"{piece} at position {position}");
            var capturedJoined = string.Join(", ", captured);
            return $"Path: {path}.  Captured pieces: {capturedJoined}.";
        }
    }
}
