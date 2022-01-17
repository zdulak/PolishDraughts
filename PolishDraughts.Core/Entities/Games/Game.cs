using System;
using System.Linq;
using PolishDraughts.Core.Entities.Players;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Exceptions;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Games
{
    public class Game : IGame
    {
        private readonly IController _controller;
        private readonly IView _view;
        private readonly Player[] _players;
        private readonly IBoard _board;
        public Game(IController controller, IView view, IBoard board, Player[] players)
        {
            _controller = controller;
            _view = view;
            _board = board;
            _players = players;

            _controller.QuitCommand += Abort;
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
                        if (player is Human)
                        {
                            _view.DisplayMsg("In order to return to main menu, please enter ctrl+c.");
                        }
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

            _view.DisplayMsg(
                winner is Color.White or Color.Black ? $"{winner} player won the game!" : "It's a draw!");
            _controller.GetExitKey();
            Abort();
        }

        public void Abort()
        {
            _board.Reset();
            _controller.QuitCommand -= Abort;
            throw new GameAbortException();
        }
    }
}
