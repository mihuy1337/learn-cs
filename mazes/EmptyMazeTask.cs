namespace Mazes;

public static class EmptyMazeTask
{
    const int SizeRobot = 1;
    const int SizeWall = 1;

    public static void MoveOut(Robot robot, int width, int height)
	{
		while (!robot.Finished)
			Move(robot, width, height);
	}

    public static void Move(Robot robot, int width, int height)
    {
        MoveTo(robot, Direction.Right, CountSteps(width));
        MoveTo(robot, Direction.Down, CountSteps(height));
    }

    public static void MoveTo(Robot robot, Direction dir, int countSteps)
	{
		for (; countSteps != 0; countSteps--) 
			robot.MoveTo(dir);
	}

    public static int CountSteps(int line) => line - SizeWall * 2 - SizeRobot;
}