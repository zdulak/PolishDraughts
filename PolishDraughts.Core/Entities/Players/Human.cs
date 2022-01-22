using System.Collections.Generic;
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

        protected override (Position piecePosition, Position targetPosition) ChooseMove()
        {
            var piecePosition = ChoosePiece();
            return (piecePosition, ChooseTargetPosition(piecePosition));
        }

        protected override CapturePath ChooseCapture(List<CapturePath> capturePaths) => _controller.GetPath(capturePaths);

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
