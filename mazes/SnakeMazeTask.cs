namespace Mazes;

public static class SnakeMazeTask
{
    const int SizeRobot = 1;
    const int SizeWall = 1;

    public static void MoveOut(Robot robot, int width, int height)
    {
        while (!robot.Finished)
            MoveLogic(robot, width, height);
    }

    public static void MoveLogic(Robot robot, int width, int height)
    {
        MoveTo(robot, Direction.Right, CountSteps(width));
        MoveTo(robot, Direction.Down, 2);
        MoveTo(robot, Direction.Left, CountSteps(width));
        if (robot.Finished)
            return;
        MoveTo(robot, Direction.Down, 2);
    }

    public static void MoveTo(Robot robot, Direction dir, int countSteps)
    {
        for (; countSteps != 0; countSteps--)
            robot.MoveTo(dir);
    }

    public static int CountSteps(int line) => line - SizeWall * 2 - SizeRobot;
}