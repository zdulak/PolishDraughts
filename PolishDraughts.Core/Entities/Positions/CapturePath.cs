using System.Collections.Generic;

namespace PolishDraughts.Core.Entities.Positions
{
    public record CapturePath(List<Position> Path, List<Position> Captured)
    {
        public override string ToString()
        {
            var path = string.Join("->", Path);
            var captured = string.Join(", ", Captured);
            return $"Path:{path}.  Captured pieces: {captured}.";
        }
    }
}
