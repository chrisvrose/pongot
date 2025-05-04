using Godot;
using System;

public partial class Main : Node2D
{


    private Score score;
    private PackedScene ballScene;

    private Timer spawnTimer;

    [Signal]
    public delegate void ScoreChangeEventHandler(int playerScore, int enemyScore);

    public override void _Ready()
    {
        ballScene = GD.Load<PackedScene>("res://objects/ball.tscn");
        score = new Score();
        spawnTimer = GetNode<Timer>("SpawnTimer");
        spawnTimer.Start();
        // StartGame();
    }

    private void onTimerStartTimeout(){
        CallDeferred(MethodName.StartGame);
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
        EmitSignal(SignalName.ScoreChange, score.player, score.enemy);
        spawnTimer.Start();
    }

}
