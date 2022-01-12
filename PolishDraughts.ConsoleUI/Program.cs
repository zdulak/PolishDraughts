using PolishDraughts.Core.Entities.Games;

namespace PolishDraughts.Presentation
{
    internal class Program
    {
        private static void Main() => new GameFactory().Create().Run();
    }
}
