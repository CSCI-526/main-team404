using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static Player;

public class PlayerLadderMoveState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerLadderMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
       
        base.Enter();
        //stateMachine.stateLocked = true;
        player.LadderMoveCtrl.climbLadder();
    }

    public override void Exit()
    {
        player.LadderMoveCtrl.leaveLadder();
        
        base.Exit();
    }

    public override bool Update()
    {

        //Debug.Log("Battle Info: "+ player.battleInfo);

        player.FlipCtrl.onHorizontalInput();
        player.rb.linearVelocityY = input.Yinput * player.ladderVerticalSpeed;

        //ladder => roll
        if (player.LevelCollisionCtrl.IsGroundDetected() && ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp()))
        {
            //player.stateMachine.stateLocked = false;
            stateMachine.ChangeState(player.rollState);
            return true;
        }
        //ladder => dash
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {

            player.stateMachine.stateLocked = false;
            stateMachine.ChangeState(player.dashState);
            return true;
        }

        //ladder =>jump
        if (input.Jump || input.isJumpBuffered)
        {
            //player.stateMachine.stateLocked = false;
            stateMachine.ChangeState(player.jumpState);
            return true;
        }

        //ladder => idle/fall
        // conflict with interact input buffer, so no buffer here
        if ((!player.ladderCheck) || input.Interact)
        {
            if (player.LevelCollisionCtrl.IsGroundDetected())
            {
                //player.stateMachine.stateLocked = false;
                stateMachine.ChangeState(player.idleState);
                return true;
            }
            else
            {
                //player.stateMachine.stateLocked = false;
                stateMachine.ChangeState(player.fallState);
                return true;
            }
        }

        return false;
        
    }
}
