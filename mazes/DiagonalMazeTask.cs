namespace Mazes;

public static class DiagonalMazeTask
{
    const int SizeRobot = 1;
    const int SizeWall = 1;

    public static void MoveOut(Robot robot, int width, int height)
    {
        while (!robot.Finished)
            LogicMove(robot, width, height);
    }

    public static void LogicMove(Robot robot, int w, int h)
    {
        if (h > w)
            MoveHorizontaly(robot, w, h);
        else
            MoveVerticaly(robot, w, h);
    }

    public static void MoveHorizontaly(Robot robot, int w, int h)
    {
        MoveTo(robot, Direction.Down, CountSteps(h) / (CountSteps(w) + 1));
        if (!robot.Finished)
            robot.MoveTo(Direction.Right);
    }

    public static void MoveVerticaly(Robot robot, int w, int h)
    {
        MoveTo(robot, Direction.Right, CountSteps(w) / (CountSteps(h) + 1));
        if (!robot.Finished)
            robot.MoveTo(Direction.Down);
    }

    public static void MoveTo(Robot robot, Direction dir, int countSteps)
    {
        for (; countSteps != 0; countSteps--)
            robot.MoveTo(dir);
    }

    public static int CountSteps(int line) => line - SizeWall * 2 - SizeRobot;
}