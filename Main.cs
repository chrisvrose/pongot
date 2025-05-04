using Godot;
using System;

public partial class Main : Node2D
{

    private int playerScore;
    private int enemyScore;
    private PackedScene ballScene;

    public override void _Ready()
    {
        ballScene = GD.Load<PackedScene>("res://objects/ball.tscn");
        playerScore = 0;
        enemyScore = 0;
        StartGame();
    }

    private void StartGame()
    {
        GD.Print("Start log");
        var ballInstance = ballScene.Instantiate<Ball>();
        AddChild(ballInstance);
        ballInstance.BallOffscreen += processBallOffScreen;
    }


    private void processBallOffScreen(bool didPlayerWin){
        if(didPlayerWin) playerScore++;
        else enemyScore++;
        GD.Print($"Score {playerScore} {enemyScore}");
        StartGame();
    }

}
