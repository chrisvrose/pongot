using Godot;
using System;

public partial class Ball : AnimatableBody2D
{
    private const float INTIAL_VELOCITY_FACTOR = 100;
    private const float SPEED_INCREASE_FACTOR = 1.01f;
    private Vector2 velocity;

    private AudioStreamPlayer2D hitAudioStreamPlayer;
    [Signal]
    public delegate void BallOffscreenEventHandler(bool didPlayerWin);


    public override void _Ready()
    {
        hitAudioStreamPlayer = GetNode<AudioStreamPlayer2D>("hitplayer");
        var realInitialVelocity = INTIAL_VELOCITY_FACTOR*Vector2.Left.Rotated((float)(GD.Randf()*Math.Tau));
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
        playSound();
        var colliderNormal = x.GetNormal();
        var reflectedVelocity = reflectVelocityAlongNormal(velocity, colliderNormal);
        this.velocity = reflectedVelocity;
    }

    private void playSound()
    {
        hitAudioStreamPlayer.PitchScale = 0.75f + GD.Randf()/8f;
        hitAudioStreamPlayer.Play();
    }

    private Vector2 reflectVelocityAlongNormal(Vector2 velocity, Vector2 colliderNormal)
    {
        var colliderNormalPartOfVelocity = velocity.Dot(colliderNormal);
        return velocity - 2 * colliderNormalPartOfVelocity * colliderNormal;
    }



    public void timerTick()
    {
        velocity.X *= SPEED_INCREASE_FACTOR;
        // GD.Print($"Speed is now {velocity}");
    }

    private void outOfScreen(){
        bool didPlayerWin = this.Position.X > 0;
        EmitSignal(SignalName.BallOffscreen,didPlayerWin);
        QueueFree();
    }
}
