using System;
using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Pieces;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Boards
{
    public class Board : IBoard
    {
        public const int Size  = 10;

        private readonly Piece[,] _slots;

        public Board()
        {
            _slots = new Piece [Size, Size];
            Reset();
        }

        public Piece this[Position position]
        {
            get => _slots[position.Row, position.Col];
            private set => _slots[position.Row, position.Col] = value;
        }

        public void Reset()
        {
            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    if (col % 2 + row % 2 == 1)
                    {
                        if (row < 4)
                        {
                            _slots[row, col] = new Men(Color.Black);
                        }

                        else if (row > 5)
                        {
                            _slots[row, col] = new Men(Color.White);
                        }

                        else
                        {
                            _slots[row, col] = null;
                        }
                    }
                }
            }
        }

        public void MovePiece(ref Position piecePosition, Position targetPosition)
        {
            (this[piecePosition], this[targetPosition]) = (this[targetPosition], this[piecePosition]);
            piecePosition = targetPosition;
        }

        public void ClearSlots(List<Position> positions) => positions.ForEach(position => this[position] = null);

        public void CrownPiece(Position position) => this[position] = new King(this[position].Color);

        public bool CanBeCrowned(Position piecePosition, Position targetPosition)
        {
            if (this[piecePosition] == null || this[piecePosition] is King) return false;
            else if (this[piecePosition].Color == Color.Black && targetPosition.Row != 9) return false;
            else if (this[piecePosition].Color == Color.White && targetPosition.Row != 0) return false;

            return true;
        }

        public bool HasPieceMove(Position piecePosition) => GetPieceMoves(piecePosition).Any();

        public bool HasPieceCapture(Position piecePosition) => GetPiecesToCapture(piecePosition).Any();
        public Position? GetPiecePosition(Piece piece)
        {
            if (piece == null) return null;

            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    if (Object.ReferenceEquals(_slots[row, col], piece))
                    {
                        return new Position(row, col);
                    }
                }
            }

            return null;
        }

        public bool IsDraw()
        {
            var colors = new[] { Color.White, Color.Black };
            return colors.All(color => HasOnlyKing(color)) || colors.All(c => !HasMove(c));
        }

        public bool HasWon(Color color)
        {
            return !HasPieces(color.Opposite()) || (HasMove(color) && !HasMove(color.Opposite()));
        }

        public void ApplyMove(Move move)
        {
            var piecePosition = move.Path.First();
            MovePiece(ref piecePosition, move.Path.Last());

            if (move.Crowned) CrownPiece(piecePosition);

            if (move.CapturedPositions != null) ClearSlots(move.CapturedPositions.ToList());
        }

        public void RevertMove(Move move)
        {
            var piecePosition = move.Path.Last();
            MovePiece(ref piecePosition, move.Path.First());

            if (move.Crowned)
            {
                this[piecePosition] = new Men(this[piecePosition].Color);
            }

            if (move.CapturedPositions != null)
            {
                for (var i = 0; i < move.CapturedPositions.Count; i++)
                {
                    this[move.CapturedPositions[i]] = move.CapturedPieces[i];
                }
            }
        }

        public List<Position> GetPiecesHavingCapture(Color color) =>
            GetPlayerPieces(color).Where(position => HasPieceCapture(position)).ToList();

        public IEnumerable<Position> GetPlayerPieces(Color color)
        {
            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    if (_slots[row, col] != null && _slots[row, col].Color == color)
                        yield return new Position(row, col);
                }
            }
        }

        public bool IsValidMove(Position piecePosition, Position targetPosition)
        {
            if (this[piecePosition] == null)
            {
                throw new ArgumentNullException(nameof(piecePosition), "There is no piece in the given slot.");
            }

            var piece = this[piecePosition];
            if (!piece.IsCorrectJump(piecePosition, targetPosition, false)) return false;

            var moveVector = (targetPosition - piecePosition).Normalize();
            while (piecePosition != targetPosition)
            {
                piecePosition += moveVector;
                if (this[piecePosition] != null) return false;
            }

            return true;
        }

        public List<Position> GetAfterCapturePositions(Position piecePosition, Position capturedPosition)
        {
            if (this[piecePosition] == null)
            {
                throw new ArgumentNullException(nameof(piecePosition), "There is no piece in the given slot.");
            }

            var targetPositions = new List<Position>();
            var moveVector = (capturedPosition - piecePosition).Normalize();
            var position = capturedPosition + moveVector;
            while (position.IsValid() && this[position] == null &&
                   this[piecePosition].IsCorrectJump(capturedPosition, position, true))
            {
                targetPositions.Add(position);
                position += moveVector;
            }

            return targetPositions;
        }

        public IEnumerable<Position> GetPieceMoves(Position piecePosition)
        {
            if (this[piecePosition] == null)
            {
                throw new ArgumentNullException(nameof(piecePosition), "There is no piece in the given slot.");
            }

            foreach (var unitMove in Piece.MoveDirections)
            {
                var position = piecePosition + unitMove;
                while (position.IsValid() &&
                       this[piecePosition].IsCorrectJump(piecePosition, position, false))
                {
                    if (this[position] == null)
                    {
                        yield return position;
                        position += unitMove;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public IEnumerable<Position> GetPiecesToCapture(Position piecePosition)
        {
            if (this[piecePosition] == null)
            {
                throw new ArgumentNullException(nameof(piecePosition), "There is no piece in the given slot.");
            }

            foreach (var unitMove in Piece.MoveDirections)
            {
                var position = piecePosition + unitMove;
                while ((position + unitMove).IsValid() &&
                       this[piecePosition].IsCorrectJump(piecePosition, position, true))
                {
                    if (this[position] == null)
                    {
                        position += unitMove;
                    }
                    else if (this[position].Color == this[piecePosition].Color.Opposite() &&
                             this[position + unitMove] == null)
                    {
                        yield return position;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public List<Move> GetPieceAllCapturePaths(Position piecePosition)
        {
            if (this[piecePosition] == null)
            {
                throw new ArgumentNullException(nameof(piecePosition), "There is no piece in the given slot.");
            }

            var allPaths = new List<Move>();
            var path = new Stack<Position>();
            path.Push(piecePosition);
            var captured = new Stack<Position>();
            ComputePieceCapturePath(piecePosition, allPaths, path, captured);
            return allPaths;
        }

        private void ComputePieceCapturePath(Position piecePosition, ICollection<Move> allPaths, Stack<Position> path,
            Stack<Position> captured)
        {
            var piecesToCapture = GetPiecesToCapture(piecePosition).ToList();
            if (!piecesToCapture.Any())
            {
                var capturedPieces = captured.Reverse().Select(p => this[p]).ToList().AsReadOnly();
                allPaths.Add(
                    new Move(
                        path.Reverse().ToList().AsReadOnly(),
                        CanBeCrowned(path.Peek(), path.Peek()),
                        captured.Reverse().ToList().AsReadOnly(),
                        capturedPieces));
                return;
            }

            foreach (var capturedPosition in piecesToCapture)
            {
                captured.Push(capturedPosition);
                this[capturedPosition].Capture();

                foreach (var afterCapturePosition in GetAfterCapturePositions(piecePosition, capturedPosition))
                {
                    var oldPosition = piecePosition;
                    path.Push(afterCapturePosition);
                    MovePiece(ref piecePosition, afterCapturePosition);

                    ComputePieceCapturePath(piecePosition, allPaths, path, captured);

                    path.Pop();
                    MovePiece(ref piecePosition, oldPosition);
                }

                this[captured.Pop()].Color = this[piecePosition].Color.Opposite();
            }
        }

        private bool HasPieces(Color color) => GetPlayerPieces(color).Any();

        public bool HasMove(Color color) => GetPlayerPieces(color).Any(position => HasPieceMove(position));

        private bool HasOnlyKing(Color color)
        {
            var pieces = GetPlayerPieces(color).ToList();
            return pieces.Count == 1 && this[pieces.First()] is King;
        }
    }
}
