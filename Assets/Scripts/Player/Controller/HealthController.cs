using UnityEngine;

public class HealthController
{
    public Player player;
    private bool levelTransitionLock = false;   

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
            if (levelTransitionLock) return;
            levelTransitionLock = true;
            player.Health -= _health;
            LevelManager.instance.StartTransitionToRestartLevel();
        }
    }
    public void GainHealth(int _health)
    {
        player.Health = Mathf.Min(player.Health + _health, player.MaxHealth);
    }
}
