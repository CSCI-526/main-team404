using System;
using UnityEngine;
using UnityEngine.Windows;

public class AirMoveController
{
    public Player player;

    public AirMoveController(Player player)
    {
        this.player = player;
    }
    
    public void OnHorizontalInput(float Xinput)
    {
        player.rb.linearVelocity = new Vector2(Xinput* player.HorizontalSpeedFalling, player.rb.linearVelocity.y);
    }

    public void Freeze()
    {
        player.rb.linearVelocity = new Vector2(0,0);
    }
}
