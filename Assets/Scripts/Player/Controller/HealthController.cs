using UnityEngine;

public class HealthController
{
    public Player player;

    public HealthController(Player _player)
    {
        this.player = _player;
    }
    public void LoseHealth(int _health)
    {
        if (player.Health > _health) 
        { 
            player.Health -= _health;
        }
        else
        {
            //Player dead reset scene
        }
    }
    public void GainHealth(int _health)
    {
        player.Health = Mathf.Min(player.Health + _health, player.MaxHealth);
    }
}
