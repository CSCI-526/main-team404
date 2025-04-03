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
        player.anim.SetBool("Deflect", true);
        if (SendToGoogle.instance != null)
        {
            SendToGoogle.instance.AddParryCount();
        }
        player.weapon0.DeactivateWeapon();
        //TODO; modify, there must be 1 frame of Fragile when deflect finished
    }

    public override void Exit()
    {
        base.Exit();
        player.DeflectCtrl.DefelectOver();
        player.AirMoveCtrl.UnFreeze();
        player.anim.SetBool("Deflect", false);
        player.weapon0.ActivateWeapon();
        player.weapon0.Equip();
        player.deflectCoolDownTimer.Set(player.deflectCoolDown);
        //TODO; modify, there must be 1 frame of Fragile when deflect finished
    }


    public override bool Update()
    {
        if( base.Update())
        {
            return true;
        }

        if(player.deflectSignal == 1)
        {
            player.ManaCtrl.AddMana(1);
            if (SendToGoogle.instance != null)
            {
                SendToGoogle.instance.AddSuccessfulParryCount();
            }
            // put frame freeze here

            // frame freeze end
            player.deflectSignal = 0;
        }
        player.AirMoveCtrl.Freeze();

        if (player.playerEmbeddedUI.animationTrigger == false)
        {
            player.stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }
}
