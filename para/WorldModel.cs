using System;

namespace GravityBalls;

public class WorldModel
{
    public double BallX;
    public double BallY;
    public double BallRadius;
    public double WorldWidth;
    public double WorldHeight;
    public double SpeedX = 150;
    public double SpeedY = 90;
    public double Resistance = 0.0001;

    public void SimulateTimeframe(double dt)
    {
        Move(dt);
        ApplyWalls();
        ApplyResistance(dt);
        Gravity();
    }
    
    private void ApplyWalls()
    {
        if (BallX + BallRadius >= WorldWidth || BallX - BallRadius <= 0)
        {
            BallX = -BallX;
        }
        if (BallY + BallRadius >= WorldHeight || BallY - BallRadius <= 0)
        {
            BallY = -BallY;
        }
    }

    private void Gravity()
    {
        BallY = BallY - BallY * 9.8;
    }

    private void ApplyResistance(double dt)
    {
        BallX -= BallX * dt * Resistance;
        BallY -= BallY * dt * Resistance;
    }

    private void Move(double dt)
    {
        BallY = Math.Min(BallY + SpeedY * dt, WorldHeight - BallRadius);
        BallX = Math.Min(BallY + SpeedX * dt, WorldWidth - BallRadius);
    }
}