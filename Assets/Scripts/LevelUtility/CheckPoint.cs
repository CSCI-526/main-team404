using Newtonsoft.Json.Bson;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isReached;
    SpriteRenderer sr;
    public bool enableDash = true;
    public bool enableComboAttack = true;

    private void Awake()
    {
        isReached = false;
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Color color = sr.color;
        color.a = 0.3f; // Set transparency (0 = fully transparent, 1 = fully opaque)
        sr.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReached)
        {

            Color color = sr.color;
            color.a = 1f; // Set transparency (0 = fully transparent, 1 = fully opaque)
            sr.color = color;

            isReached = true;

            // send google form, clear form if needed
            if (SendToGoogle.instance != null)
            {
                //very simple and lazy way to generate a "id" of the checkpoint for now.
                //porb should replace this part in future
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;

                SendToGoogle.instance.UpdateCheckEnds(x * 10000 + y);
            }

            // Send last good position to player
            PlayerInfo.instance.player.LastGoodPosition = transform.position;
            PlayerInfo.instance.player.Health = PlayerInfo.instance.player.MaxHealth;


            PlayerInfo.instance.player.canDash = enableDash;
            PlayerInfo.instance.player.canComboAttack = enableComboAttack;


        }
    }
}
