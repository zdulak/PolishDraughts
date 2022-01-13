using System.Collections.Generic;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class Human : Player
    {
        private readonly IController _controller;

        public Human(IBoard board, Color color, IController controller) : base(board, color)
        {
            _controller = controller;
        }

        protected override Position ChoosePiece()
        {
            while (true)
            {
                var piecePosition = _controller.GetPosition(1);
                if (Board[piecePosition] != null && Board[piecePosition].Color == Color)
                {
                    if(Board.HasPieceMove(piecePosition)) return piecePosition;

                    _controller.View.DisplayMsg("The piece does not have any move.");
                }
                else
                {
                    _controller.View.DisplayMsg("Invalid slot");
                }
            }
        }

        protected override Position ChooseTargetPosition(Position piecePosition)
        {
            while (true)
            {
                var targetPosition = _controller.GetPosition(2);
                if (Board.IsValidMove(piecePosition, targetPosition)) return targetPosition;

                _controller.View.DisplayMsg("Invalid move");
            }
        }

        protected override CapturePath ChooseFromList(List<CapturePath> capturePaths)
        {
            return _controller.GetPath(capturePaths);

            //Alternative version
            //if (capturePaths.Count > 1)
            //{
            //    return _controller.GetPath(capturePaths);
            //}

            //_controller.View.DisplayMsg($"You have a mandatory capture. {capturePaths.First()}");
            //return capturePaths.First();
        }
    }
}
