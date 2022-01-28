using System;
using System.Linq;
using PolishDraughts.Core.Entities.Players;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Core.Entities.Games
{
    public class Game : IGame
    {
        private readonly IBoard _board;
        private readonly IController _controller;
        private readonly Player[] _players;
        private readonly Action _returnPoint;
        private readonly IView _view;

        public Game(IController controller, IView view, IBoard board, Player[] players, Action returnPoint)
        {
            _controller = controller;
            _view = view;
            _board = board;
            _players = players;
            _returnPoint = returnPoint;
        }

        public void Run()
        {
            _controller.QuitCommand += Abort;

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
                    if (_board.HasMove(player.Color))
                    {
                        _view.DisplayMsg($"{player.Color} player turn.");
                        if (player is Human)
                        {
                            _view.DisplayMsg("In order to return to main menu, please enter quit.");
                        }
                        player.MakeMove();
                        _view.DisplayBoard(_board);
                    }

                    if (_board.IsDraw())
                    {
                        isPlayed = false;
                        break;
                    }

                    if (_board.HasWon(player.Color))
                    {
                        winner = player.Color;
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
            _returnPoint();
        }
    }
}
