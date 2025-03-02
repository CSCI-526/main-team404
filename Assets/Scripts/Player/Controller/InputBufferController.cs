using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class InputBufferController
{
    public Player player;

    public InputBufferController(Player player)
    {
        this.player = player;
    }

    public void Initialize(Player player)
    {
        this.player = player;
    }

    public void SetBufferOnInput()
    {
        if (player.input.Jump)
        {
            player.input.SetJumpBufferTimer();
        }
        if (player.input.Roll)
        {
            player.input.SetRollBufferTimer();
        }
        if (player.input.Attack)
        {
            player.input.SetAttackBufferTimer();
        }
    }
}
