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

        public virtual void MakeMove()
        {
            Position piecePosition;
            var piecesHavingCapture = Board.GetPiecesHavingCapture(Color);

            if (piecesHavingCapture.Count > 0)
            {
                var allPaths = piecesHavingCapture.SelectMany(p => Board.GetPieceAllCapturePaths(p)).ToList();
                var maxCaptured = allPaths.Max(ps => ps.Captured.Count);
                var maximalCapturePaths = allPaths.Where(ps => ps.Captured.Count == maxCaptured).ToList();
                var capturePath = ChooseFromList(maximalCapturePaths);

                piecePosition = capturePath.Path.First();
                Board.MovePiece(ref piecePosition, capturePath.Path.Last());
                Board.ClearSlots(capturePath.Captured);
            }
            else
            {
                piecePosition = ChoosePiece();
                var targetPosition = ChooseTargetPosition(piecePosition);
                Board.MovePiece(ref piecePosition, targetPosition);
            }

            if (Board.CanBeCrowned(piecePosition)) Board.CrownPiece(piecePosition);
        }

        protected abstract Position ChoosePiece();
        protected abstract Position ChooseTargetPosition(Position piecePosition);
        protected abstract CapturePath ChooseFromList(List<CapturePath> capturePaths);

    }
}
