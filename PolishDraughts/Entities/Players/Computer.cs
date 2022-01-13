using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolishDraughts.Core;
using PolishDraughts.Core.Entities.Boards;
using PolishDraughts.Core.Entities.Players;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;

namespace PolishDraughts
{
    public class Computer : Player
    {
        private readonly View _view;
        private readonly Random _random;
        public Computer(Board board, Color color, View view) : base(board, color)
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
