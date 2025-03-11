using UnityEngine;

public class RegionEnabler : MonoBehaviour
{
    public GameObject objectRelated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Enable the region
            if (!objectRelated.activeSelf)
            {
                objectRelated.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable the region
            if (objectRelated == null) return;
            if (objectRelated.activeSelf)
            {
                objectRelated.SetActive(false);
            }
        }
    }
}
