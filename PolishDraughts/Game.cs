using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolishDraughts
{
    public class Game
    {
        private readonly View _view;
        private readonly Player[] _players;
        private readonly Board _board;
        public Game(View view, Board board, Player[] players)
        {
            _view = view;
            _board = board;
            _players = players;
        }
        public void Run()
        {
            _view.Clear();
            _view.DisplayBoard(_board);
            _view.DisplayMsg("The game starts!");

            var isPlayed = true;
            // By default winner is set to draw;
            var winner = Color.None;
            while (isPlayed)
            {
                foreach (var player in _players)
                {
                    if (!player.HasPieces())
                    {
                        winner = player.Color.Opposite();
                        isPlayed = false;
                        break;
                    }

                    if (_players.All(p => p.HasOnlyKing()))
                    {
                        isPlayed = false;
                        break;
                    }

                    if (player.HasMove())
                    {
                        _view.DisplayMsg($"{player.Color} player turn.");
                        player.MakeMove();
                        _view.DisplayBoard(_board);
                    }
                    else
                    {
                        // Check if the another player has a move.
                        if (_players.First(p => p.Color != player.Color).HasMove())
                        {
                            winner = player.Color.Opposite();
                        }

                        isPlayed = false;
                        break;
                    }
                }
            }

            if (winner == Color.White || winner == Color.Black)
            {
                _view.DisplayMsg($"{winner} player won the game!");
            }
            else
            {
                _view.DisplayMsg("It's a draw!");
            }

            Quit();
        }

        public void Quit() => Environment.Exit(0);

    }
}
