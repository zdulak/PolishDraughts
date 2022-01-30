using System;
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
        private const int MaxValue = 2 * IBoard.Size * KingValue;

        public MinimaxAi(Color color, IBoard board, IView view) : base(color, board, view)
        {
        }

        protected override Move GetComputerMove(List<Move> moves)
        {
            Move finalMove = null;
            // Utility bigger than an extreme value guarantees that it will change value at least once
            var utility = Color == Color.White ? -MaxValue - 1 : MaxValue + 1;
            foreach (var move in moves)
            {
                Board.ApplyMove(move);
                var moveUtility = GetMoveUtility(Color.Opposite(), 4, -MaxValue - 1, MaxValue + 1);
                //Console.WriteLine($"Utility {moveUtility}: {move}");
                Board.RevertMove(move);

                if ((Color == Color.White && moveUtility > utility) || (Color == Color.Black && moveUtility < utility))
                {
                    utility = moveUtility;
                    finalMove = move;
                }
            }

            return finalMove;
        }

        private int GetMoveUtility(Color playerColor, int depth, int whiteBestUtility, int blackBestUtility)
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
            // The initial value of the utility equal to a defeat guarantees that
            // no move will be excluded from considerations 
            var utility = playerColor == Color.White ? -MaxValue : MaxValue;
            foreach (var move in GetMoves(playerColor))
            {
                Board.ApplyMove(move);
                var moveUtility = GetMoveUtility(
                    playerColor.Opposite(),
                    depth - 1,
                    whiteBestUtility,
                    blackBestUtility);
                Board.RevertMove(move);

                if (playerColor == Color.White && moveUtility > utility)
                {
                    utility = moveUtility;
                    whiteBestUtility = Math.Max(utility, whiteBestUtility);
                }
                else if(playerColor == Color.Black && moveUtility < utility)
                {
                    utility = moveUtility;
                    blackBestUtility = Math.Min(utility, blackBestUtility);
                }

                if (blackBestUtility < whiteBestUtility)
                {
                    return utility;
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