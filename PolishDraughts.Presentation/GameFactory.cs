using System;
using PolishDraughts.Core.Entities.Players;
using PolishDraughts.Core.Enums;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Presentation
{
    // Do not confuse the following code with the factory design pattern.
    // See https://refactoring.guru/design-patterns/factory-comparison
    public class GameFactory
    {
        private readonly Func<Player[], IGame> _gameFactory;
        private readonly Func<Color, Human> _humanFactory;
        private readonly Func<Color, Computer> _computerFactory;


        public GameFactory(Func<Player[], IGame> gameFactory, Func<Color, Human> humanFactory,
            Func<Color, Computer> computerFactory)
        {
            _gameFactory = gameFactory;
            _humanFactory = humanFactory;
            _computerFactory = computerFactory;
        }

        public IGame Create() => _gameFactory(CreatePlayers());


        private Player[] CreatePlayers()
        {
            while (true)
            {
                Console.WriteLine(
                    @"Please select a game mode. Enter:
1 - Computer vs Computer
2 - Computer (white pieces) vs Human (black pieces)
3 - Human (white pieces) vs Computer (black pieces)
4 - Human vs Human");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        return new Player[]
                        {
                            _computerFactory(Color.White), _computerFactory(Color.Black)
                        };
                    case "2":
                        return new Player[]
                        {
                            _computerFactory(Color.White), _humanFactory(Color.Black)

                        };
                    case "3":
                        return new Player[]
                        {
                            _humanFactory(Color.White), _computerFactory(Color.Black)
                        };
                    case "4":
                        return new Player[]
                        {
                            _humanFactory(Color.White), _humanFactory(Color.Black)
                        };
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
    }
}
