using Godot;
using System;

public partial class Main : Node2D
{


    private int playerScore;
    private int enemyScore;
    private PackedScene ballScene;

    private Timer spawnTimer;
    private Timer speedUpTimer;

    [Signal]
    public delegate void ScoreChangeEventHandler(int playerScore, int enemyScore);

    public override void _Ready()
    {
        ballScene = GD.Load<PackedScene>("res://objects/ball.tscn");
        spawnTimer = GetNode<Timer>("SpawnTimer");
        speedUpTimer = GetNode<Timer>("SpeedupTimer");

        playerScore = 0;
        enemyScore = 0;
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
            playerScore++;
        }
        else
        {
            enemyScore++;
        }
        EmitSignal(SignalName.ScoreChange, playerScore, enemyScore);
        spawnTimer.Start();
    }

}
