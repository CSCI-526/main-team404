using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class LanceDrop : EnemyHitBoxBase
{
    public Rigidbody2D rb;
    public SpriteRenderer circle;
    public float frequency;
    public int isActive; // isActive is equvilant to is Attacking
    //TODO: Mechanism to prevent Dereferencing Null dfTransform pointer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
    }


    public override int getID () { return 0; }

    public override void playerDestroy(int _param)
    {
        base.playerDestroy(_param);
        result = _param;
    }
    public override int getAmmo()
    {
        return 3;
    }
}
