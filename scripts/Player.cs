using Godot;
using System;

public partial class Player : AnimatableBody2D
{
    
    public const float speed = 200;
    public const float outOfBound = 600/(2*2) - 32/2;
    public override void _Ready(){
    }
    public override void _PhysicsProcess(double delta)
    {
        float floatDelta = (float)delta;


        var input = Input.GetAxis("move_up", "move_down");
        // GD.Print($"input {input}");
        moveBatWithInput(input, floatDelta);
    }

    private void moveBatWithInput(float input, float floatDelta)
    {
        var position = this.Position;


        float deltaPosition = input * speed * floatDelta;
        var newPositionY = position.Y + deltaPosition;
        // GD.Print($"We were at {position.Y} wanting to move with {deltaPosition} and will now try to go to {newPositionY}");
        position.Y = Mathf.Clamp(newPositionY, -outOfBound, outOfBound);

        this.Position = position;
    }
}
