using Godot;
using System;

public partial class Ball : AnimatableBody2D
{
    private readonly Vector2 initialVelocity = Vector2.Left * 100;
    private const float speedFactor = 1.01f;

    private Vector2 velocity;
    Random r;


    [Signal]
    public delegate void BallOffscreenEventHandler(bool didPlayerWin);


    public override void _Ready()
    {
        r = new Random();
        var realInitialVelocity = initialVelocity.Rotated((float)(r.NextSingle()*Math.Tau));
        velocity = realInitialVelocity;

    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D x = this.MoveAndCollide(velocity * (float)delta);
        if (x is not null)
        {
            onCollisionDetect(x);
        }
        // this.ApplyCentralImpulse(colliderNormal*2);
    }

    private void onCollisionDetect(KinematicCollision2D x)
    {

        var colliderNormal = x.GetNormal();
        
        var reflectedVelocity = reflectVelocityAlongNormal(velocity, colliderNormal);
        this.velocity = reflectedVelocity;
    }

    private Vector2 reflectVelocityAlongNormal(Vector2 velocity, Vector2 colliderNormal)
    {
        var colliderNormalPartOfVelocity = velocity.Dot(colliderNormal);
        return velocity - 2 * colliderNormalPartOfVelocity * colliderNormal;
    }



    public void timerTick()
    {
        velocity.X *= speedFactor;
        // GD.Print($"Speed is now {velocity}");
    }

    private void outOfScreen(){
        bool didPlayerWin = this.Position.X > 0;
        EmitSignal(SignalName.BallOffscreen,didPlayerWin);
        QueueFree();
    }
}
