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
            Position piecePosition;
            Move move;
            var piecesHavingCapture = Board.GetPiecesHavingCapture(Color);

            if (piecesHavingCapture.Count > 0)
            {
                var allPaths = piecesHavingCapture.SelectMany(p => Board.GetPieceAllCapturePaths(p)).ToList();
                var maxCaptured = allPaths.Max(ps => ps.CapturedPositions.Count);
                var maximalCapturePaths = allPaths.Where(ps => ps.CapturedPositions.Count == maxCaptured).ToList();
                var capturePath = ChooseCapturePath(maximalCapturePaths);

                piecePosition = capturePath.Path.First();
                Board.MovePiece(ref piecePosition, capturePath.Path.Last());
                Board.ClearSlots(capturePath.CapturedPositions.ToList());
                move = capturePath;
            }
            else
            {
                Position targetPosition;
                (piecePosition, targetPosition) = ChooseSimpleMove();
                move = new Move(new List<Position> { piecePosition, targetPosition }.AsReadOnly());
                Board.MovePiece(ref piecePosition, targetPosition);
            }

            if (Board.CanBeCrowned(piecePosition))
            {
                Board.CrownPiece(piecePosition);
                move = move with { Crowned = true };
            }

            return move;
        }

        protected abstract (Position PiecePosition, Position TargetPosition) ChooseSimpleMove();
        protected abstract Move ChooseCapturePath(List<Move> capturePaths);

    }
}
