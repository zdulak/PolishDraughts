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

        protected override Position ChoosePiece()
        {
            var piecesWithMove = Board.GetPlayerPieces(Color)
                .Where(p => Board.HasPieceMove(p))
                .ToList();
            return GetPlayerChoiceFromList(piecesWithMove);
        }

        protected override Position ChooseTargetPosition(Position piecePosition)
        {
            var pieceMoves = Board.GetPieceMoves(piecePosition).ToList();
            var targetPosition = GetPlayerChoiceFromList(pieceMoves);
            _view.DisplayMsg("Computer makes a move: " + $"{piecePosition}->{targetPosition}");
            return targetPosition;
        }

        protected override CapturePath ChooseFromList(List<CapturePath> capturePaths)
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
    }
}
