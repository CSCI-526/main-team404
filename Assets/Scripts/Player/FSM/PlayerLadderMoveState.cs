using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerLadderMoveState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerLadderMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
       
        base.Enter();
        player.LadderMoveCtrl.climbLadder();
    }

    public override void Exit()
    {
        player.LadderMoveCtrl.leaveLadder();
        
        base.Exit();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.FlipCtrl.onHorizontalInput();
        player.rb.linearVelocityY = input.Yinput * player.ladderVerticalSpeed;

        //ladder =>jump
        if (input.Jump || input.isJumpBuffered)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }

        //ladder => idle/fall
        // conflict with interact input buffer, so no buffer here
        if (!player.ladderCheck || input.Interact)
        {
            if (player.LevelCollisionCtrl.IsGroundDetected())
            {
                stateMachine.ChangeState(player.idleState);
                return true;
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
                return true;
            }
        }

        return false;
        
    }
}
