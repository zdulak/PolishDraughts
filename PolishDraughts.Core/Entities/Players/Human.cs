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

        protected override Move ChooseMove(List<Move> moves) => moves.First().CapturedPositions != null
            ? ChooseCapturePath(moves)
            : ChooseSimpleMove(moves);

        private Move ChooseSimpleMove(List<Move> moves)
        {
            var piecePosition = ChoosePiece(moves);
            return new Move(
                new List<Position>()
                    { piecePosition, ChooseTargetPosition(piecePosition, moves) }.AsReadOnly());
        }

        private Move ChooseCapturePath(List<Move> capturePaths) => _controller.GetPath(capturePaths);

        private Position ChoosePiece(List<Move> moves)
        {
            var piecesWithMove = moves.Select(m => m.Path.First()).ToList();
            while (true)
            {
                var piecePosition = _controller.GetPosition(1);
                if (Board[piecePosition] != null && Board[piecePosition].Color == Color)
                {
                    if (piecesWithMove.Contains(piecePosition)) return piecePosition;

                    _controller.View.DisplayMsg("The piece does not have any move.");
                }
                else
                {
                    _controller.View.DisplayMsg("Invalid slot");
                }
            }
        }

        private Position ChooseTargetPosition(Position piecePosition, List<Move> moves)
        {
            while (true)
            {
                var targetPosition = _controller.GetPosition(2);
                var path = new List<Position>() { piecePosition, targetPosition };
                if (moves.Any(m => m.Path.SequenceEqual(path))) return targetPosition;

                _controller.View.DisplayMsg("Invalid move");
            }
        }
    }
}
