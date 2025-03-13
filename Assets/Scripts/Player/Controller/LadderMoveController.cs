using UnityEngine;

public class LadderMoveController
{
    Player player;

    public LadderMoveController(Player _player)
    {
        player = _player;
    }

    public void climbLadder()
    {
        player.rb.gravityScale = 0f;
        player.JumpCtrl.ResetCounter(2);
        // set player postion to current spear
        player.transform.position = player.currentInteractingSpear.GetValidPosition(player.transform.position);
        player.rb.linearVelocity = new Vector2(0,player.rb.linearVelocity.y);
        player.currentInteractingSpear.TurnOnTopMargin();
        player.currentInteractingSpear.displayClimbUI();
        player.weapon0.UnEquip();
    }

    public void leaveLadder()
    {
        player.rb.gravityScale = player.gravityScale;
        if (player.currentInteractingSpear != null)
        {
            player.currentInteractingSpear.TurnOffTopMargin();
            player.currentInteractingSpear.stopDisplayClimbUI();
        }
        player.weapon0.Equip();
    }
}
