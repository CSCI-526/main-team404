using UnityEngine;

public class PlayerDamagedPenalyState : PlayerState
{
    public PlayerDamagedPenalyState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.KnockBackCtrl.Prep();
        // change player color
        player.Bleeding.color = Color.red;
    }

    public override void Exit()
    {
        base.Exit();
        player.battleInfo = Player.BattleInfo.Peace;
        // change player color
        player.Bleeding.color = Color.gray;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }
        player.KnockBackCtrl.ApplyKnockback();

        if (player.KnockBackCtrl.timer.TimeUp())
        {
            stateMachine.ChangeState(player.idleState);
            return true;
        }
        return false;
    }
}
