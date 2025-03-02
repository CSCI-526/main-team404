using UnityEngine;

public class PlayerDeflectState : PlayerState
{
    public PlayerDeflectState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        input.isDeflectBuffered = false;
        player.DeflectCtrl.Deflect();
        //TODO; modify, there must be 1 frame of Fragile when deflect finished
    }

    public override void Exit()
    {
        base.Exit();
        player.DeflectCtrl.DefelectOver();
        //TODO; modify, there must be 1 frame of Fragile when deflect finished
    }


    public override bool Update()
    {
        if( base.Update())
        {
            return true;
        }

        player.AirMoveCtrl.Freeze();

        if (player.DeflectCtrl.timer.TimeUp())
        {
            player.stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }
}
