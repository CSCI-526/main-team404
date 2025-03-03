using Unity.VisualScripting;
using UnityEngine;

public class WeaponController
{
    Player player;


    public WeaponController(Player player)
    {
        this.player = player;
    }

    public void UseGrabSkill()
    {

    }
    public void Attack(AttackInfo ai)
    {
        player.currentWeapon.attack(ai);
    }
    public void Skill(AttackInfo ai)
    {
        
    }
    public void switchWP1()
    {
        if(player.weapon1 == null)
        {
            return;
        }
        PlayerWeapon temp = player.currentWeapon;
        player.currentWeapon.DeactivateWeapon();
        player.currentWeapon = player.weapon1;
        player.weapon1 = temp;
        player.currentWeapon.ActivateWeapon();
    }

    public void SetCurrentWP(int id)
    {
        // if player does not have weapon
        if (player.currentWeapon == null)
        {
            player.currentWeapon = player.weaponDictionary.SearchWeapon(id);
            player.currentWeapon.ActivateWeapon();
            return;
        }
        // if player current weapon is not the same as grabbed weapon
        // put grabbed weapon in current weapon and swich current weapon to first emply weapon slot
        // if there is no weapon slot
        // pub grabbed weapon in bag
        // TODO: implement
    }

}
