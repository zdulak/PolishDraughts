using System;

namespace PolishDraughts
{
    // Do not confuse the following code with the factory design pattern.
    // See https://refactoring.guru/design-patterns/factory-comparison
    public class GameFactory
    {
        private readonly View _view;
        private Board _board;
        private Controller _controller;

        public GameFactory() => _view = new View();

        public Game Create()
        {
            _board = new Board();
            return new Game(_view, _board, CreatePlayers());
        }

        private Controller CreateController() => new Controller(_view);

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
                            new Computer(_board, Color.White, _view),
                            new Computer(_board, Color.Black, _view)
                        };
                    case "2":
                        return new Player[]
                        {
                            new Computer(_board, Color.White, _view),
                            new Human(_board, Color.Black, _controller ??= CreateController())

                        };
                    case "3":
                        return new Player[]
                        {
                            new Human(_board, Color.White, _controller ??= CreateController()),
                            new Computer(_board, Color.Black, _view)
                        };
                    case "4":
                        _controller ??= CreateController();
                        return new Player[]
                        {
                            new Human(_board, Color.White, _controller),
                            new Human(_board, Color.Black, _controller)
                        };
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
    }
}
