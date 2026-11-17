using System;
using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{
    public static double[,] MedianFilter(double[,] original)
    {
        var rows = original.GetLength(0);
        var columns = original.GetLength(1);
        var medianPixels = new double[rows, columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                medianPixels[i, j] = GetMedian(original, i, j, rows, columns);

        return medianPixels;
    }

    private static double GetMedian(double[,] image, int x, int y, int rows, int columns)
    {
        var neighbors = new List<double>();

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int nx = x + dx;
                int ny = y + dy;

                if (nx >= 0 && nx < rows && ny >= 0 && ny < columns)
                    neighbors.Add(image[nx, ny]);
            }
        }

        neighbors.Sort();
        int count = neighbors.Count;

        if (count % 2 == 1)
            return neighbors[count / 2];

        return (neighbors[count / 2 - 1] + neighbors[count / 2]) / 2.0;
    }
}