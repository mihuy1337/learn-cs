using System;
using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        int rows = original.GetLength(0);
        int columns = original.GetLength(1);
        int totalPixels = rows * columns;
        int whiteCount = (int)(whitePixelsFraction * totalPixels);

        if (whiteCount <= 0)
            return new double[rows, columns];

        double threshold = CalculateThreshold(original, totalPixels, whiteCount);
        return ApplyThreshold(original, threshold);
    }

    private static double CalculateThreshold(double[,] original, int totalPixels, int whiteCount)
    {
        var pixels = new List<double>(totalPixels);
        foreach (var pixel in original)
            pixels.Add(pixel);

        pixels.Sort();
        int thresholdIndex = Math.Max(0, totalPixels - whiteCount);
        return pixels[thresholdIndex];
    }

    private static double[,] ApplyThreshold(double[,] original, double threshold)
    {
        int rows = original.GetLength(0);
        int columns = original.GetLength(1);
        var result = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
                result[i, j] = original[i, j] >= threshold ? 1.0 : 0.0;
        }

        return result;
    }
}