using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    public static class Drawer
    {
        private static float x;
        private static float y;
        private static IGraphics graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            graphics = newGraphics;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawStep(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Move(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double rotationAngle, IGraphics graphics)
        {
            Drawer.Initialize(graphics);

            var size = Math.Min(width, height);
            var diagonalLength = Math.Sqrt(2) * (size * 0.375f + size * 0.04f) / 2;
            var startX = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var startY = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Drawer.SetPosition(startX, startY);

            var pen = new Pen(Brushes.Yellow);
            DrawSide(pen, size, 0);
            DrawSide(pen, size, -Math.PI / 2);
            DrawSide(pen, size, Math.PI);
            DrawSide(pen, size, Math.PI / 2);
        }

        private static void DrawSide(Pen pen, float size, double angle)
        {
            var sideLength = size * 0.375f;
            var offsetLength = size * 0.04f;

            Drawer.DrawStep(pen, sideLength, angle);
            Drawer.DrawStep(pen, offsetLength * Math.Sqrt(2), angle + Math.PI / 4);
            Drawer.DrawStep(pen, sideLength, angle + Math.PI);
            Drawer.DrawStep(pen, sideLength - offsetLength, angle + Math.PI / 2);

            Drawer.Move(offsetLength, angle - Math.PI);
            Drawer.Move(offsetLength * Math.Sqrt(2), angle + 3 * Math.PI / 4);
        }
    }
}