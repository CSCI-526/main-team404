using UnityEngine;

public class PlayerGrabState : PlayerState
{
    public PlayerGrabState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        input.isGrabBuffered = false;
        player.GrabCtrl.Grab();

        //Use grab key for now to send data
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.SetTime((int)Time.time);
            SendToGoogle.instance.Send();
            SendToGoogle.instance.ResetAll();
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.GrabCtrl.GrabOver();
    }


    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        if (player.GrabCtrl.timer.TimeUp())
        {
            player.stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }
}
