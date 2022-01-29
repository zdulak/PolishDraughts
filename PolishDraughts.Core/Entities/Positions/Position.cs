using System;
using PolishDraughts.Core.Interfaces;
using static System.Math;

namespace PolishDraughts.Core.Entities.Positions
{
    public readonly struct Position
    {
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        // It creates a position from a input in the algebraic notation.
        public Position(string algebraicPosition)
        {
            Row = IBoard.Size - int.Parse(algebraicPosition[1..]);
            Col = Convert.ToInt32(algebraicPosition[0]) - 97;
        }

        public int Row { get; }
        public int Col { get; }

        // Position vector normalized in the Chebyshev norm, which generates chessboard metric.
        public Position Normalize() => new Position(Row / Abs(Max(Row, Col)), Col / Abs(Max(Row, Col)));

        public bool IsValid() =>
            Row >= 0 && Row < IBoard.Size && Col >= 0 && Col < IBoard.Size && Row % 2 + Col % 2 == 1;

        //Position in the algebraic notation.
        public override string ToString() => $"{(char)(Col + 97)}{IBoard.Size - Row}";

        public bool Equals(Position other) => Row == other.Row && Col == other.Col;

        public override bool Equals(object obj) => obj is Position other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Row, Col);

        public static bool operator ==(Position x, Position y) => x.Equals(y);

        public static bool operator !=(Position x, Position y) => !(x == y);

        public static Position operator +(Position x, Position y) => new Position(x.Row + y.Row, x.Col + y.Col);

        public static Position operator -(Position x, Position y) => new Position(x.Row - y.Row, x.Col - y.Col);

        //public static Position operator *(int lambda, Position p) => new Position(lambda * p.Row, lambda * p.Col);

        //public static Position operator *(Position p, int lambda) => lambda * p;

        //public int To1DCoordinate() => (Col + Row % 2 - 1) / 2 + 5 * Row;

        //public Position(int position1D)
        //{
        //    Row = position1D / 5;
        //    Col = 2 * (position1D - 5 * Row) + 1 - Row % 2;
        //}
    }
}
