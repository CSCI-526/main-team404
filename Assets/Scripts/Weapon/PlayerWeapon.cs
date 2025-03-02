using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public Player player;
    public int WeaponID;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void grabSkill(){ }
    public virtual void attack(AttackInfo ai) { }
    public virtual void skill(AttackInfo ai) { }

    public virtual void ActivateWeapon()
    {
        gameObject.SetActive(true);
    }

    public virtual void DeactivateWeapon()
    {
        gameObject.SetActive(false);
    }
}
