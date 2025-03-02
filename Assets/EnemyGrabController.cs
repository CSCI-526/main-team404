using System.Collections.Generic;
using UnityEngine;

public class EnemyGrabController : EnemyHitBoxBase
{
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public override int getID()
    {
        return 0;
    }

    public override void playerDestroy(int _param)
    {
        base.playerDestroy(_param);
        result = _param;
    }
    public override int getAmmo()
    {
        return 3;
    }

    private void OnEnable()
    {
        result = 0;
    }

}
