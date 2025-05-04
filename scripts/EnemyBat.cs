using Godot;
using System;

public partial class EnemyBat : AnimatableBody2D
{
    public const float speed = 200;
    public const float outOfBound = 600 / (2 * 2) - 32 / 2;
    private readonly float factor = 1 / 150f;
    public Ball ballNode = null;


    public override void _Ready()
    {
    }
    public override void _PhysicsProcess(double delta)
    {
        float floatDelta = (float)delta;

        moveBatFromBallPosition(floatDelta);
    }

    private void moveBatFromBallPosition(float floatDelta)
    {
        float deltaPosition = getDeltaYFromBall();
        if (deltaPosition == float.NegativeInfinity)
        {
            return;
        }

        var position = this.Position;

        var newPositionY = Mathf.MoveToward(position.Y, position.Y + deltaPosition, floatDelta * speed);
        position.Y = Mathf.Clamp(newPositionY, -outOfBound, outOfBound);

        this.Position = position;
    }

    private float getDeltaYFromBall()
    {
        if (ballNode == null) { return float.NegativeInfinity; }

        return Mathf.Clamp((ballNode.Position - Position).Dot(Vector2.Down), -1, 1);
    }




    void detectBodyEnter(Node2D enteredNode)
    {
        if (enteredNode is Ball)
        {
            ballNode = (Ball)enteredNode;
        }
    }
    void detectBodyExit(Node2D exitNode)
    {
        if (exitNode is Ball)
        {
            ballNode = null;
        }
    }
}
