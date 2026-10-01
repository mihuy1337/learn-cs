using System;

namespace Rectangles;

public static class RectanglesTask 
{
    public static int GetLenIntersect(int start1, int end1, int start2, int end2)
    {
        int left = Math.Max(start1, start2);
        int right = Math.Min(end1, end2);
        return right - left;
    }
    // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
    public static bool AreIntersected(Rectangle r1, Rectangle r2)
    {
        return GetLenIntersect(r1.Left, r1.Right, r2.Left, r2.Right) >= 0
            && GetLenIntersect(r1.Top, r1.Bottom, r2.Top, r2.Bottom) >= 0;
    }

    // Площадь пересечения прямоугольников
    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
    {
        var width = GetLenIntersect(r1.Left, r1.Right, r2.Left, r2.Right);
        var height = GetLenIntersect(r1.Top, r1.Bottom, r2.Top, r2.Bottom);

        if (width <= 0 || height <= 0)
            return 0;

        return width * height;
    }

    // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
    // Иначе вернуть -1
    // Если прямоугольники совпадают, можно вернуть номер любого из них.
    public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
    {
        static bool IsInner(Rectangle inner, Rectangle outer)
        {
            return (inner.Left >= outer.Left) && (inner.Top >= outer.Top) 
                && (inner.Right <= outer.Right) && (inner.Bottom <= outer.Bottom);
        }

        if (IsInner(r1, r2))
            return 0;
        else if (IsInner(r2, r1))
            return 1;
        else return -1;
    }
}