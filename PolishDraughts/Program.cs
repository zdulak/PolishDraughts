using System;

namespace PolishDraughts
{
    internal class Program
    {
        private static void Main() => new GameFactory().Create().Run();
    }
}
