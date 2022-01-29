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

        protected abstract Move GetComputerMove(List<Move> moves);

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
        protected override List<Move> GetMoves()
        {
            var piecesHavingCapture = Board.GetPlayerPiecesHavingCapture(Color);
            return piecesHavingCapture.Count > 0 ? GetAllCaptureMoves(piecesHavingCapture) : GetAllSimpleMoves();
        }

        protected List<Move> GetAllSimpleMoves()
        {
            //Local function is introduced for a code readability.
            IEnumerable<Move> GetAllPieceMoves(Position piecePosition)
            {
                return Board.GetPieceSimpleMoves(piecePosition)
                    .Select(
                        targetPosition =>
                            new Move(
                                new List<Position> { piecePosition, targetPosition }.AsReadOnly(),
                                Board.CanBeCrowned(piecePosition, targetPosition)));
            }

            var piecesWithMove = Board.GetPlayerPieces(Color).Where(p => Board.HasPieceSimpleMove(p));
            return piecesWithMove.SelectMany(piecePosition => GetAllPieceMoves(piecePosition)).ToList();
        }
    }
}
