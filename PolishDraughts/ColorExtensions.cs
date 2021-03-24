using System;

namespace PolishDraughts
{
    public static class ColorExtensions
    {
        public static Color Opposite(this Color color)
        {
            return color switch
            {
                Color.White => Color.Black,
                Color.Black => Color.White,
                Color.None => Color.None,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
