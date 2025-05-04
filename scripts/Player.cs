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
        Vector2 moveNewVec = input * speed * floatDelta * Vector2.Down;
        MoveAndCollide(moveNewVec);
    }
    
}
