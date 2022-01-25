using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class Human : Player
    {
        private readonly IController _controller;

        public Human(Color color, IBoard board, IController controller) : base(color, board)
        {
            _controller = controller;
        }

        protected override List<Move> GetAllMoves()
        {
            var piecesHavingCapture = Board.GetPiecesHavingCapture(Color);
            return piecesHavingCapture.Count > 0 ? GetAllCaptureMoves(piecesHavingCapture) : null;
        }

        protected override Move ChooseMove(List<Move> moves) => moves?.FirstOrDefault()?.CapturedPositions != null
            ? ChooseCapturePath(moves)
            : ChooseSimpleMove();

        private Move ChooseCapturePath(List<Move> capturePaths) => _controller.GetPath(capturePaths);

        private Move ChooseSimpleMove()
        {
            var piecePosition = ChoosePiece();
            var targetPosition = ChooseTargetPosition(piecePosition);
            return new Move(
                new List<Position>()
                    { piecePosition, targetPosition }.AsReadOnly(), Board.CanBeCrowned(targetPosition));
        }

        private Position ChoosePiece()
        {
            while (true)
            {
                var piecePosition = _controller.GetPosition(1);
                if (Board[piecePosition] != null && Board[piecePosition].Color == Color)
                {
                    if (Board.HasPieceMove(piecePosition)) return piecePosition;

                    _controller.View.DisplayMsg("The piece does not have any move.");
                }
                else
                {
                    _controller.View.DisplayMsg("Invalid slot");
                }
            }
        }
        private Position ChooseTargetPosition(Position piecePosition)
        {
            while (true)
            {
                var targetPosition = _controller.GetPosition(2);
                if (Board.IsValidMove(piecePosition, targetPosition)) return targetPosition;

                _controller.View.DisplayMsg("Invalid move");
            }
        }
    }
}
