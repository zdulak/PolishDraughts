using System;
using System.Collections.Generic;
using System.Linq;
using PolishDraughts.Core.Entities.Players;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Presentation
{
    public class Menu
    {
        private readonly IView _view;
        private readonly IController _controller;
        private readonly Func<Player[], Action, IGame> _gameFactory;
        private readonly Func<Color, Human> _humanFactory;
        private readonly Func<Color, Computer> _computerFactory;
        private readonly Player[] _players;

        public Menu(IView view, IController controller, Func<Player[], Action, IGame> gameFactory, Func<Color, Human> humanFactory,
            Func<Color, Computer> computerFactory)
        {
            _view = view;
            _controller = controller;
            _gameFactory = gameFactory;
            _humanFactory = humanFactory;
            _computerFactory = computerFactory;
            _players = new Player[2];
        }

        public void MainMenu()
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
            var options = new List<string> { "Human", "Computer", nameof(MainMenu) };

            foreach (var (color, i) in new[] { (Color.White, 0), (Color.Black, 1) })
            {
                _view.Clear();

                var optionNumber = _controller.GetOption(
                    options.Count,
                    () =>
                    {
                        _view.DisplayPlayerChoice(color);
                        _view.DisplayChoiceMenu(options);
                    }, true);

                switch (optionNumber)
                {
                    case 0:
                        _players[i] = _humanFactory(color);
                        break;
                    case 1:
                        _players[i] = _computerFactory(color);
                        break;
                    default:
                        MainMenu();
                        break;
                }
            }

            _gameFactory(_players, MainMenu).Run();
        }

        private void Rules()
        {
            ChoiceMenu(
                new List<Action>
                {
                    MainMenu,  _controller.Quit
                },
                _view.DisplayRules);
        }

        private void About()
        {
            ChoiceMenu(
                new List<Action>
                {
                    MainMenu, _controller.Quit
                },
                _view.DisplayAbout);
        }

        private void ChoiceMenu(List<Action> options, Action menuView)
        {
            _view.Clear();
            var optionNumber = _controller.GetOption(options.Count, () =>
            {
                menuView();
                _view.DisplayChoiceMenu(options.Select(o => o.Method.Name));
            }, true);
            options[optionNumber]();
        }
    }
}