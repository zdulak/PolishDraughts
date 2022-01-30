using System.Collections.Generic;
using PolishDraughts.Core.Entities.Pieces;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;

namespace PolishDraughts.Core.Interfaces
{
    public interface IBoard : IDependency
    {
        const int Size = 10;
        Piece this[Position position] { get; }
        void Reset();
        void ApplyMove(Move move);
        void RevertMove(Move move);
        bool IsDraw();
        bool HasWon(Color color);
        bool HasMove(Color color);
        List<Position> GetPlayerPiecesHavingCapture(Color color);
        IEnumerable<Position> GetPlayerPieces(Color color);
        bool CanBeCrowned(Position piecePosition, Position targetPosition);
        bool IsValidSimpleMove(Position piecePosition, Position targetPosition);
        bool HasPieceSimpleMove(Position piecePosition);
        IEnumerable<Move> GetPieceSimpleMoves(Position piecePosition);
        List<Move> GetPieceAllCapturePaths(Position piecePosition);
       
        //Position? GetPiecePosition(Piece piece);
    }
}