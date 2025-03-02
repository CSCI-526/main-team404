using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isAttackBuffered = false;
        if (player.currentWeapon == null)
        {
            //if there is not weapon, nothing happen;
            return;
        }
        if (input.Yinput >0 || input.isUpBuffered)
        {
            player.WeaponCtrl.Attack(new AttackInfo(0b01));
            return;
        }
        if(input.Yinput < 0 || input.isDownBuffered)
        {
            player.WeaponCtrl.Attack(new AttackInfo(0b10));
            return;
        }
        player.WeaponCtrl.Attack(new AttackInfo(0b00));
    }

    public override void Exit()
    {
        base.Exit();
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
        stateMachine.ChangeState(player.idleState);
        return true;
        //return false;
    }
}
