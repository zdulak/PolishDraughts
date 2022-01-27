using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Pieces;
using PolishDraughts.Core.Entities.Positions;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Players
{
    public class MinimaxAi : Computer
    {
        private const int KingValue = 4;
        private const int MaxValue = 2 * Boards.Board.Size * KingValue;

        public MinimaxAi(Color color, IBoard board, IView view) : base(color, board, view)
        {
        }

        protected override Move GetComputerMove(List<Move> moves)
        {
            Move finalMove = null;
            var utility = Color == Color.White ? -MaxValue : MaxValue;
            foreach (var move in moves)
            {
                Board.ApplyMove(move);
                var moveUtility = GetMoveUtility(Color.Opposite(), 3);
                Board.RevertMove(move);

                if ((Color == Color.White && moveUtility > utility) || (Color == Color.Black && moveUtility < utility))
                {
                    utility = moveUtility;
                    finalMove = move;
                }
            }

            return finalMove;
        }

        private int GetMoveUtility(Color playerColor, int depth)
        {
            if (Board.IsDraw())
            {
                return 0;
            }

            if (Board.HasWon(playerColor.Opposite()))
            {
                return playerColor.Opposite() == Color.White ? MaxValue : -MaxValue;
            }

            if (depth < 1)
            {
                return Utility();
            }

            var utility = playerColor == Color.White ? -MaxValue : MaxValue;
            foreach (var move in GetMoves())
            {
                Board.ApplyMove(move);
                var moveUtility = GetMoveUtility(playerColor.Opposite(), depth - 1);
                Board.RevertMove(move);

                if ((playerColor == Color.White && moveUtility > utility) ||
                    (playerColor == Color.Black && moveUtility < utility))
                {
                    utility = moveUtility;
                }
            }

            return utility;
        }

        private int Utility()
        {
            return new[] { Color.White, Color.Black }
                .Sum(color => Board.GetPlayerPieces(color)
                    .Select(position => Board[position])
                    .Sum(piece => (color == Color.White ? 1 : -1) * (piece is King ? KingValue : 1)));
        }
    }
}