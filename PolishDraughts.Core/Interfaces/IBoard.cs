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
        void MovePiece(ref Position piecePosition, Position targetPosition);
        void ClearSlots(List<Position> positions);
        void CrownPiece(Position position);
        bool CanBeCrowned(Position position);
        bool HasPieceMove(Position piecePosition);
        bool HasPieceCapture(Position piecePosition);
        Position? GetPiecePosition(Piece piece);
        List<Position> GetPiecesHavingCapture(Color color);
        IEnumerable<Position> GetPlayerPieces(Color color);
        bool IsValidMove(Position piecePosition, Position targetPosition);
        List<Position> GetAfterCapturePositions(Position piecePosition, Position capturedPosition);
        IEnumerable<Position> GetPieceMoves(Position piecePosition);
        IEnumerable<Position> GetPiecesToCapture(Position piecePosition);
        List<CapturePath> GetPieceAllCapturePaths(Position piecePosition);
    }
}