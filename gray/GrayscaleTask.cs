namespace Recognizer;

public static class GrayscaleTask
{
	public static double[,] ToGrayscale(Pixel[,] original)
	{
		var rows = original.GetLength(0);
		var columns = original.GetLength(1);

        double[,] grayScale = new double[rows, columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                grayScale[i, j] = GetBright(original[i, j]);

		return grayScale;
	}

	private static double GetBright(Pixel p) => (0.299 * p.R + 0.587 * p.G + 0.114 * p.B) / 255;
}
