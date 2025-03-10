using System.Collections;
using UnityEngine;

public class DrpSpearVertical : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum SpearState
    {
        InAir,
        OnGround
    }

    public enum SpearType
    {
        Up,
        Down,
    }
    public SpearType type;
    public SpearState state;
    public float moveSpeed;
    public float liveTime;
    public Transform wallCheckPosition;
    public float wallCheckDistance;
    public LayerMask wallLayer;
    private Rigidbody2D rb;
    public GameObject attackBox;
    public GameObject topMargin;
    public Transform boundPosition;
    public TMPro.TextMeshProUGUI climbNote;
    public TMPro.TextMeshProUGUI stopClimbNote;
    public SpriteRenderer tip;
    public SpriteRenderer body;
    void Start()
    {
        state = SpearState.InAir;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(nameof(DestroySpearCoroutine));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SpearState.InAir:
                rb.linearVelocity = -transform.up * moveSpeed;
                checkCollision();
                break;
            case SpearState.OnGround:
                break;
        }
    }

    private void checkCollision()
    {
        if (state == SpearState.OnGround)
        {
            return;
        }
        bool isWallDetected = Physics2D.Raycast(wallCheckPosition.position, wallCheckPosition.up, wallCheckDistance, wallLayer);
        if (isWallDetected)
        {
            state = SpearState.OnGround;
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //transform.parent = other.transform;
            attackBox.SetActive(false);
            //topMargin.SetActive(true);
            StartCoroutine(nameof(DisableAttacBoxCoroutine));
        }
    }

    //IEnumerator DestroySpearVCoroutine()
    //{
    //    yield return new WaitForSeconds(liveTime);
    //    Destroy(gameObject);
    //}

    IEnumerator DisableAttacBoxCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        attackBox.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheckPosition.position, wallCheckPosition.position + wallCheckPosition.up * wallCheckDistance);
    }

    public void TurnOffTopMargin()
    {
        topMargin.SetActive(false);
    }
    public void TurnOnTopMargin()
    {
        topMargin.SetActive(true);
    }

    public void displayUI()
    {
        climbNote.gameObject.SetActive(true);
    }

    public void stopDisplayUI()
    {
        climbNote.gameObject.SetActive(false);
    }

    public void displayClimbUI()
    {
        climbNote.gameObject.SetActive(false);
        stopClimbNote.gameObject.SetActive(true);
    }

    public void stopDisplayClimbUI()
    {
        stopClimbNote.gameObject.SetActive(false);
        climbNote.gameObject.SetActive(true);
    }

    public void SetColor(Color color)
    {
        tip.color = color;
        body.color = color;
    }

    public Vector2 GetValidPosition(Vector2 position)
    {
        switch (type)
        {
            case SpearType.Up:
                if (position.y < boundPosition.position.y)
                {
                    return new Vector2(transform.position.x, boundPosition.position.y);
                }
                else
                {
                    return new Vector2(transform.position.x, position.y);
                }
            case SpearType.Down:
                if (position.y > boundPosition.position.y)
                {
                    return new Vector2(transform.position.x, boundPosition.position.y);
                }
                else
                {
                    return new Vector2(transform.position.x, position.y);
                }


        }
        return Vector2.zero;
    }

    IEnumerator DestroySpearCoroutine()
    {
        float flashDuration = 0.2f;
        int numberOfFlashes = 3; 
        yield return new WaitForSeconds(liveTime);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            SetColor(Color.red);
            yield return new WaitForSeconds(flashDuration);
            SetColor(Color.white);
            yield return new WaitForSeconds(flashDuration);
        }
        Destroy(gameObject);
    }
}
