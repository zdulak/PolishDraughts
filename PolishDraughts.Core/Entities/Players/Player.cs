using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Pieces;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public abstract class Player
    {
        public Color Color { get; }

        protected readonly IBoard Board;
        protected Player(Color color, IBoard board)
        {   
            Board = board;
            Color = color;
        }

        public Move MakeMove()
        {
            var moves = GetMoves();
            var move = ChooseMove(moves);
            Board.ApplyMove(move);
            return move;
        }

        protected abstract Move ChooseMove(List<Move> moves);

        protected virtual List<Move> GetMoves()
        {
            var piecesHavingCapture = Board.GetPiecesHavingCapture(Color);
            return piecesHavingCapture.Count > 0 ? GetAllCaptureMoves(piecesHavingCapture) : null;
        }

        protected List<Move> GetAllCaptureMoves(List<Position> piecesHavingCapture)
        {
            var allPaths = piecesHavingCapture.SelectMany(p => Board.GetPieceAllCapturePaths(p)).ToList();
            var maxCaptured = allPaths.Max(move => move.CapturedPositions.Count);
            var maximalCapturePaths = allPaths.Where(ps => ps.CapturedPositions.Count == maxCaptured).ToList();
            return maximalCapturePaths;
        }
    }
}
