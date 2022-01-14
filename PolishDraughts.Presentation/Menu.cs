using System;
using System.Collections.Generic;
using PolishDraughts.Core.Entities.Players;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Presentation
{
    public class Menu
    {
        private readonly IView _view;
        private readonly IController _controller;
        private readonly Func<Player[], IGame> _gameFactory;
        private readonly Func<Color, Human> _humanFactory;
        private readonly Func<Color, Computer> _computerFactory;
        private readonly Player[] _players;

        public Menu(IView view, IController controller, Func<Player[], IGame> gameFactory, Func<Color, Human> humanFactory,
            Func<Color, Computer> computerFactory)
        {
            _view = view;
            _controller = controller;
            _gameFactory = gameFactory;
            _humanFactory = humanFactory;
            _computerFactory = computerFactory;
            _players = new Player[2];
        }

        public void Main()
        {
            ChoiceMenu(
                new List<Action>
                {
                    NewGame, Rules, About, _controller.Quit
                },
                _view.DisplayMainMenu);
        }

        private void NewGame()
        {

        }

        private void Rules()
        {
            ChoiceMenu(
                new List<Action>
                {
                    Main,  _controller.Quit
                },
                _view.DisplayRules);
        }

        private void About()
        {
            ChoiceMenu(
                new List<Action>
                {
                    Main, _controller.Quit
                },
                _view.DisplayAbout);
        }

        private void ChoiceMenu(List<Action> options, Action menuView)
        {
            var optionNumber = _controller.GetOption(options.Count, menuView);
            options[optionNumber]();
        }
    }
}