using System;

namespace Fractals;

    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var random = new Random(seed);
            double x = 1;
            double y = 0;

            for (int i = 0; i < iterationsCount; i++)
            {
                double angle = random.Next(2) == 0
                    ? Math.PI / 4
                    : 3 * Math.PI / 4;

                double shiftX = angle == Math.PI / 4 ? 0 : 1;

                (x, y) = Transform(x, y, angle, shiftX);

                pixels.SetPixel(x, y);
            }
        }

        private static (double x, double y) Transform(
            double x, double y, double angle, double shiftX)
        {
            double sqrt2 = Math.Sqrt(2);

            double newX = (x * Math.Cos(angle) - y * Math.Sin(angle))
                          / sqrt2 + shiftX;

            double newY = (x * Math.Sin(angle) + y * Math.Cos(angle))
                          / sqrt2;

            return (newX, newY);
        }
    }
