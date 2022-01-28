using System.Collections.Generic;
using PolishDraughts.Core.Entities.Pieces;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;

namespace PolishDraughts.Core.Interfaces
{
    public interface IBoard : IDependency
    {
        Piece this[Position position] { get; }
        void Reset();
        bool IsDraw();
        bool HasWon(Color color);
        void ApplyMove(Move move);
        void RevertMove(Move move);
        void MovePiece(ref Position piecePosition, Position targetPosition);
        void ClearSlots(List<Position> positions);
        void CrownPiece(Position position);
        bool CanBeCrowned(Position piecePosition, Position targetPosition);
        bool HasPieceMove(Position piecePosition);
        bool HasPieceCapture(Position piecePosition);
        Position? GetPiecePosition(Piece piece);
        bool IsValidMove(Position piecePosition, Position targetPosition);
        List<Position> GetAfterCapturePositions(Position piecePosition, Position capturedPosition);
        IEnumerable<Position> GetPieceMoves(Position piecePosition);
        IEnumerable<Position> GetPiecesToCapture(Position piecePosition);
        List<Move> GetPieceAllCapturePaths(Position piecePosition);
        public bool HasMove(Color color);
        List<Position> GetPiecesHavingCapture(Color color);
        IEnumerable<Position> GetPlayerPieces(Color color);
    }
}