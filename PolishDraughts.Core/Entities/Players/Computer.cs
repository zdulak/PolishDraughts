using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public abstract class Computer : Player
    {
        private readonly IView _view;
        public Computer(Color color, IBoard board, IView view) : base(color, board)
        {
            _view = view;
        }

        protected override List<Move> GetMoves()
        {
            var piecesHavingCapture = Board.GetPiecesHavingCapture(Color);
            return piecesHavingCapture.Count > 0 ? GetAllCaptureMoves(piecesHavingCapture) : GetAllSimpleMoves();
        }

        protected override Move ChooseMove(List<Move> moves)
        {
            var move = GetComputerMove(moves);
            if (move.CapturedPieces == null)
            {
                _view.DisplayMsg("Computer makes a move: " + $"{move.Path.First()}->{move.Path.Last()}");
            }
            else
            {
                _view.DisplayMsg($"Computer makes a mandatory capture: {move}");
            }

            return move;
        }

        protected List<Move> GetAllSimpleMoves()
        {
            //Local function is introduced for a code readability.
            IEnumerable<Move> GetAllPieceMoves(Position piecePosition)
            {
                return Board.GetPieceMoves(piecePosition)
                    .Select(
                        targetPosition =>
                            new Move(
                                new List<Position> { piecePosition, targetPosition }.AsReadOnly(),
                                Board.CanBeCrowned(piecePosition, targetPosition)));
            }

            var piecesWithMove = Board.GetPlayerPieces(Color).Where(p => Board.HasPieceMove(p));
            return piecesWithMove.SelectMany(piecePosition => GetAllPieceMoves(piecePosition)).ToList();
        }

        protected abstract Move GetComputerMove(List<Move> moves);
    }
}
