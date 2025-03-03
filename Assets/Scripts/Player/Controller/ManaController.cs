using UnityEngine;

public class ManaController
{
    public Player player;

    public ManaController(Player _player)
    {
        this.player = _player;
    }
    public void Initialize(int initialMana, int maxMana)
    {
        player.Mana = initialMana;
        player.MaxMana = maxMana;
    }
    public void AddMana(int mana)
    {
        player.Mana = Mathf.Min(player.Mana + mana, player.MaxMana);
    }
    //return true if mana is enough, and cost mana
    //return false if mana is not enough
    public bool CostMana(int mana)
    {
        if(player.Mana >= mana)
        {
            player.Mana -= mana;
            return true;
        }
        return false;
    }
}
