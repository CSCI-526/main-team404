using System.Collections;
using UnityEngine;

public class DraupnirSpear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum SpearState
    {
        InAir,
       
        OnWall
       
    }
    
    public SpearState state;
    public float moveSpeed;
    public float liveTime;
    public Transform wallCheckPosition;
    public float wallCheckDistance;
    public LayerMask wallLayer;
    public GameObject attackBox;
    private Rigidbody2D rb;
    private PlatformEffector2D pe;


    void Start()
    {
        state = SpearState.InAir;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        pe = GetComponent<PlatformEffector2D>();
        StartCoroutine(nameof(DestroySpearCoroutine));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SpearState.InAir:
                rb.linearVelocity = transform.right*moveSpeed;
                checkCollision();
                break;
            case SpearState.OnWall:
                break;
        }
    }
    private void checkCollision()
    {
        if (state == SpearState.OnWall)
        {
            return;
        }
        bool isWallDetected = Physics2D.Raycast(wallCheckPosition.position, wallCheckPosition.right, wallCheckDistance, wallLayer);
        if (isWallDetected)
        {
            state = SpearState.OnWall;
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //transform.parent = other.transform;
            attackBox.SetActive(false);
            StartCoroutine(nameof(DisableAttacBoxCoroutine));
        }
    }
    //private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    //{
    //    //Debug.Log(collision.gameObject.name);
    //    if (state == SpearState.OnWall)
    //    {
    //        return;
    //    }
        
    //    if (collision.collider.CompareTag("Wall"))
    //    {
    //        state = SpearState.OnWall;
    //        rb.linearVelocity = Vector2.zero;
    //        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    //        transform.parent = collision.transform;
    //        StartCoroutine(nameof(DisableAttacBoxCoroutine));

    //    }

    //}

    

    IEnumerator DestroySpearCoroutine()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }

    IEnumerator DisableAttacBoxCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        attackBox.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheckPosition.position, wallCheckPosition.position + wallCheckPosition.right * wallCheckDistance);
    }


}
