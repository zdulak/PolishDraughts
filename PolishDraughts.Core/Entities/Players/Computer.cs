using System;
using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class Computer : Player
    {
        private readonly IView _view;
        private readonly Random _random;
        public Computer(Color color, IBoard board, IView view) : base(color, board)
        {
            _view = view;
            _random = new Random();
        }

        protected override (Position piecePosition, Position targetPosition) ChooseMove()
        {
            var move = GetPlayerChoiceFromList(GetAllPlayerMoves().ToList());
            _view.DisplayMsg("Computer makes a move: " + $"{move.piecePosition}->{move.targetPosition}");
            return move;
        }

        protected override CapturePath ChooseCapture(List<CapturePath> capturePaths)
        {
            var capturePath = GetPlayerChoiceFromList(capturePaths);
            _view.DisplayMsg($"Computer makes a mandatory capture: {capturePath}");
            return capturePath;
        }

        private T GetPlayerChoiceFromList<T>(IReadOnlyList<T> elements)
        {
            System.Threading.Thread.Sleep(300);
            return elements[_random.Next(elements.Count)];
        }

        private IEnumerable<(Position piecePosition, Position targetPosition)> GetAllPlayerMoves()
        {
            //Local function is introduced for a code readability.
            IEnumerable<(Position piecePosition, Position targetPosition)> GetAllPieceMoves(Position piecePosition)
            {
                return Board.GetPieceMoves(piecePosition)
                    .SelectMany(
                        targetPosition => new (Position, Position)[] { (piecePosition, targetPosition) });
            }

            var  piecesWithMove = Board.GetPlayerPieces(Color).Where(p => Board.HasPieceMove(p));
            return piecesWithMove.SelectMany(
                piecePosition => GetAllPieceMoves(piecePosition));
        }
    }
}
