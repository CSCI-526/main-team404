using Unity.Cinemachine;
using UnityEngine;

public class Lance : PlayerWeapon
{
    [Header("Component")]
    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject skin;
    public GameObject throwableLancePrefabHorizontal;
    public GameObject throwableLancePrefabVertical;
    public LayerMask ground;


    [Header("Initialization")]
    public Transform spwanPoint;
    public Transform shootingPostion;

    [Header("Timer")]
    public float timer;

    [Header("Stats")]
    public bool isDisappear;

    #region States
    public LanceStateMachine stateMachine;
    public LanceIdleState idleState;
    public LanceAttackState attackState;
    public LanceDisappearState disappearState;
    #endregion

    private void Awake()
    {
        WeaponID = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        isDisappear = false;

        stateMachine = new LanceStateMachine(this);
        idleState = new LanceIdleState(stateMachine,this);
        attackState = new LanceAttackState(stateMachine, this);
        disappearState = new LanceDisappearState(stateMachine, this);

        stateMachine.Initialize(idleState);

    }

    private void OnEnable()
    {
        transform.position = spwanPoint.position;
    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        timerUpdate();
    }

    private void timerUpdate()
    {
        timer = Mathf.Max(0,timer-Time.deltaTime);
    }

    public override void attack(AttackInfo ai)
    {
        
        base.attack(ai);
        if (!isDisappear)
        {
            stateMachine.ChangeState(attackState,ai);
        }
    }

    public override void grabSkill()
    {
        base.grabSkill();
    }
    public override void skill(AttackInfo ai)
    {
        base.skill(ai);
    }

    public override void ActivateWeapon()
    {
        base.ActivateWeapon();
    }

    public override void DeactivateWeapon()
    {
        base.DeactivateWeapon();
    }

    public void Disappear()
    {
        col.enabled = false;
        skin.SetActive(false);
        isDisappear = true;
    }

    public void Appear()
    {
        col.enabled = true;
        skin.SetActive(true);
        isDisappear = false ;
    }
}
