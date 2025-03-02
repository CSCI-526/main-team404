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
        if(player.currentWeaponAmmo > 0)
        {
            player.currentWeapon.attack(ai);
            player.currentWeaponAmmo--;
        }
        //TODO: cost mana to attack when there is no ammo
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
    public void switchWP2()
    {
        if (player.weapon2 == null)
        {
            return;
        }
        PlayerWeapon temp = player.currentWeapon;
        player.currentWeapon.DeactivateWeapon();
        player.currentWeapon = player.weapon2;
        player.weapon2 = temp;
        player.currentWeapon.ActivateWeapon();
    }

    public void SetCurrentWP(EnemyHitBoxBase grab)
    {
        // if player does not have weapon
        if (player.currentWeapon == null)
        {
            player.currentWeapon = player.weaponDictionary.SearchWeapon(grab.getID());
            player.currentWeaponAmmo = grab.getAmmo();
            player.currentWeapon.ActivateWeapon();
            return;
        }
        // if player current weapon is the same as grabbed weapon, reload
        if (player.currentWeapon.WeaponID == grab.getID())
        {
            player.currentWeaponAmmo = grab.getAmmo();
            player.currentWeapon.ActivateWeapon();
            return;
        }
        // if player current weapon is not the same as grabbed weapon
        // if there is at least 1 weapon slot
        // optionA: put grabbed weapon in first empty weapon slot
        // optionB: put grabbed weapon in current weapon and swich current weapon to first emply weapon slot
        // if there is not weapon slot
        // optionA,B continue: pop memu to let player choose which weapon to abandon
        // TODO: implement
    }

    //public void UpdateCurrentWPAmmo()
    //{
    //    if(player.currentWeaponAmmo == 0)
    //    {
    //        player.currentWeapon.DeactivateWeapon();
    //    }
    //}

}
