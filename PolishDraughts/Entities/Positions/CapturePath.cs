using System.Collections.Generic;

namespace PolishDraughts.Core.Entities.Positions
{
    public class CapturePath
    {
        public List<Position> Path { get; set; }
        public List<Position> Captured { get; set; }
        public override string ToString()
        {
            var path = string.Join("->", Path);
            var captured = string.Join(", ", Captured);
            return $"Path:{path}.  Captured pieces: {captured}.";
        }
    }
}
