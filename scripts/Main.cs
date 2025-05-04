using Godot;
using System;

public partial class Main : Node2D
{


    private Score score;
    private PackedScene ballScene;

    private Timer spawnTimer;
    private Timer speedUpTimer;

    [Signal]
    public delegate void ScoreChangeEventHandler(int playerScore, int enemyScore);

    public override void _Ready()
    {
        ballScene = GD.Load<PackedScene>("res://objects/ball.tscn");
        score = new Score();
        spawnTimer = GetNode<Timer>("SpawnTimer");
        speedUpTimer = GetNode<Timer>("SpeedupTimer");
        spawnTimer.Start();
    }

    private void onTimerStartTimeout(){
        CallDeferred(MethodName.StartGameByLoadingBall);
    }

    private void StartGameByLoadingBall()
    {
        var ballInstance = ballScene.Instantiate<Ball>();
        AddChild(ballInstance);
        ballInstance.BallOffscreen += processBallOffScreen;
        speedUpTimer.Timeout += ballInstance.timerTick;
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
