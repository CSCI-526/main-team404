using UnityEngine;

public class GroundMoveController
{
    
    public Player player;

    public GroundMoveController(Player player)
    {
        this.player = player;
    }

    public void onHorizontalInput(float Xinput)
    {

        // stand on spear
        if (player.transform.parent!= null)
        {
            Vector2 parentSpeed = player.transform.parent.GetComponent<Rigidbody2D>().linearVelocity;
            player.rb.linearVelocity = new Vector2(Xinput * player.HorizontalSpeedGround, player.rb.linearVelocity.y) + parentSpeed;
        }
        else
        {
            player.rb.linearVelocity = new Vector2(Xinput*player.HorizontalSpeedGround, player.rb.linearVelocity.y);

        }
    }
    public void Freeze()
    {
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
    }
    
}
