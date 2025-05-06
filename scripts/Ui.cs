using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Ui : Control
{
    Label enemyLabel;
    Label playerLabel;

    public override void _Ready()
    {
        enemyLabel = GetNode<Label>("EnemyScore");
        playerLabel = GetNode<Label>("PlayerScore");
    }

    public void onPlayerScoreUpdate(int player, int enemy)
    {
        playerLabel.Text = player.ToString();
        enemyLabel.Text = enemy.ToString();
    }
}
