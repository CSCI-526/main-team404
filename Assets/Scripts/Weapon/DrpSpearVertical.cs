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

    public enum SpearUse
    {
        Player,
        Level
    }
    public SpearType type;
    public SpearState state;
    public SpearUse useType;
    public float moveSpeed;
    public float liveTime;
    public Transform wallCheckPosition;
    public float wallCheckDistance;
    public LayerMask wallLayer;
    private Rigidbody2D rb;
    public GameObject attackBox;
    public Transform boundPosition;
    public TMPro.TextMeshProUGUI climbNote;
    public TMPro.TextMeshProUGUI stopClimbNote;
    public SpriteRenderer tip;
    public SpriteRenderer body;
    public Color normal;
    public Color mount;
    private LookAtPlayerWhenActive magnet;
    public float customLayeredDeadZone = 0.01f;
    void Start()
    {
        state = SpearState.InAir;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        magnet = GetComponentInChildren<LookAtPlayerWhenActive>();

        if (useType == SpearUse.Level)
        {
            state = SpearState.OnGround;
            StuckToLevel();
        }
        else
        {
            StartCoroutine(nameof(DestroySpearCoroutine));
        }
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
                if (PlayerInfo.instance.player.currentInteractingSpear == this)
                {
                    if (Mathf.Abs(PlayerInfo.instance.playerPosition.x - transform.position.x) < PlayerInfo.instance.player.ladderCenterDeadZone + customLayeredDeadZone)
                    {
                        if (PlayerInput.instance.Xinput == 0)
                        {
                            SetColor(mount);
                            if (magnet.isActive)
                            {
                                magnet.Hide();
                            }
                        }
                        else
                        {
                            SetColor(normal);
                            if (!magnet.isActive)
                            {
                                magnet.Show();
                            }
                        }
                    }
                    else
                    {
                        SetColor(normal);
                        if (!magnet.isActive)
                        {
                            magnet.Show();
                        }
                    }
                }
                else
                {
                    SetColor(normal);
                    if (magnet.isActive)
                    {
                        magnet.Hide();
                    }
                }
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
            StuckToLevel();
        }
    }

    private void StuckToLevel()
    {
        state = SpearState.OnGround;
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //transform.parent = other.transform;
        attackBox.SetActive(false);
        //topMargin.SetActive(true);
        StartCoroutine(nameof(DisableAttacBoxCoroutine));
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


    public void SetColor(Color color)
    {
        tip.color = color;
        body.color = color;
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
