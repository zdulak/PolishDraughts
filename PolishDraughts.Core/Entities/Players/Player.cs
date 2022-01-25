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

        public bool HasPieces() => Board.GetPlayerPieces(Color).Any();

        public bool HasMove() => Board.GetPlayerPieces(Color).Any(p => Board.HasPieceMove(p));

        public bool HasOnlyKing()
        {
            var pieces = Board.GetPlayerPieces(Color).ToList();
            return pieces.Count == 1 && Board[pieces.First()] is King;
        }

        public virtual Move MakeMove()
        {
            var moves = GetAllMoves();
            var move = ChooseMove(moves);
            Board.ApplyMove(move);
            return move;
        }

        protected abstract Move ChooseMove(List<Move> moves);

        protected virtual List<Move> GetAllMoves()
        {
            var piecesHavingCapture = Board.GetPiecesHavingCapture(Color);
            return piecesHavingCapture.Count > 0 ? GetAllCaptureMoves(piecesHavingCapture) : GetAllSimpleMoves();
        }

        protected List<Move> GetAllCaptureMoves(List<Position> piecesHavingCapture)
        {
            var allPaths = piecesHavingCapture.SelectMany(p => Board.GetPieceAllCapturePaths(p)).ToList();
            var maxCaptured = allPaths.Max(move => move.CapturedPositions.Count);
            var maximalCapturePaths = allPaths.Where(ps => ps.CapturedPositions.Count == maxCaptured).ToList();
            return maximalCapturePaths;
        }

        protected List<Move> GetAllSimpleMoves()
        {
            //Local function is introduced for a code readability.
            IEnumerable<Move> GetAllPieceMoves(Position piecePosition)
            {
                return Board.GetPieceMoves(piecePosition)
                    .Select(
                        targetPosition =>
                            new Move(
                                new List<Position> { piecePosition, targetPosition }.AsReadOnly(),
                                Board.CanBeCrowned(targetPosition)));
            }

            var piecesWithMove = Board.GetPlayerPieces(Color).Where(p => Board.HasPieceMove(p));
            return piecesWithMove.SelectMany(piecePosition => GetAllPieceMoves(piecePosition)).ToList();
        }
    }
}
