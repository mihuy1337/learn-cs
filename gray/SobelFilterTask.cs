using System;

namespace Recognizer;

internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];

        var kernelSize = sx.GetLength(0);
        var shift = kernelSize / 2;

        for (int x = shift; x < width - shift; x++)
        {
            for (int y = shift; y < height - shift; y++)
                result[x, y] = ComputeSobelPixel(g, sx, x, y, shift);
        }

        return result;
    }

    private static double ComputeSobelPixel(double[,] g, double[,] sx, int x, int y, int shift)
    {
        double gx = 0;
        double gy = 0;
        int kernelSize = sx.GetLength(0);

        for (int i = 0; i < kernelSize; i++)
        {
            for (int j = 0; j < kernelSize; j++)
            {
                int imgX = x - shift + i;
                int imgY = y - shift + j;
                double pixel = g[imgX, imgY];

                gx += sx[i, j] * pixel;
                gy += sx[j, i] * pixel;
            }
        }

        return Math.Sqrt(gx * gx + gy * gy);
    }
}