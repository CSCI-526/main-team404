using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isRollBuffered = false;
        player.RollCtrl.Prep();
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.linearVelocity = new Vector2(0,player.rb.linearVelocity.y);

    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.RollCtrl.Rolling();

        //roll => idle
        if (player.RollCtrl.rollDurationTimer.TimeUp())
        {
            stateMachine.ChangeState(player.idleState);
            return true;
        }
        return false;
        
    }

}
