using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //TODO: abandon ontriggerEnter2D, use manual collision detection instead
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            player.ladderCheck = true;
            player.currentInteractingSpear = collision.GetComponent<DrpSpearVertical>();
            player.currentInteractingSpear.displayUI();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            player.ladderCheck = false;
            player.currentInteractingSpear.stopDisplayUI();
            player.currentInteractingSpear = null;
        }
    }
}
