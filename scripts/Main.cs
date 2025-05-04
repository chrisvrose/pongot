using Godot;
using System;

public partial class Main : Node2D
{


    private Score score;
    private PackedScene ballScene;

    [Signal]
    public delegate void ScoreChangeEventHandler(int playerScore, int enemyScore);

    public override void _Ready()
    {
        ballScene = GD.Load<PackedScene>("res://objects/ball.tscn");
        score = new Score();
        StartGame();
    }

    private void StartGame()
    {
        var ballInstance = ballScene.Instantiate<Ball>();
        AddChild(ballInstance);
        ballInstance.BallOffscreen += processBallOffScreen;
    }


    private void processBallOffScreen(bool didPlayerWin)
    {
        if (didPlayerWin)
        {
            score.player++;
        }
        else
        {
            score.enemy++;
        }
        GD.Print($"Score {score}");
        EmitSignal(SignalName.ScoreChange, score.player, score.enemy);
        StartGame();
    }

}
