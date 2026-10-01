using System;

namespace DistanceTask;

public static class DistanceTask
{
    public static double GetLenVector(double x1, double y1, double x2, double y2)
    {
        var lenX = Math.Max(x1, x2) - Math.Min(x1, x2);
        var lenY = Math.Max(y1, y2) - Math.Min(y1, y2);

        return Math.Sqrt(lenX * lenX + lenY * lenY);
    }

    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
        if (ax == bx && ay == by)
            return GetLenVector(x, y, ax, ay);

        if ((bx - ax) * (x - ax) + (by - ay) * (y - ay) >= 0 && 
            (ax - bx) * (x - bx) + (ay - by) * (y - by) >= 0)
        {
            var AB = GetLenVector(ax, ay, bx, by);
            var AC = GetLenVector(ax, ay, x, y);
            var BC = GetLenVector(bx, by, x, y);
            var perimetr = (AB + AC + BC) / 2.0;
            return 2 * Math.Sqrt(perimetr * (perimetr -  AB) * (perimetr - AC) * (perimetr - BC)) / AB;
        }
        else 
            return Math.Min(GetLenVector(x, y, ax, ay), GetLenVector(x, y, bx, by));
    }
}