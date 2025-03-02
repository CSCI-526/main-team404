using Unity.Behavior;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMe()
    {
        GetComponent<BehaviorGraphAgent>().enabled = false;
        Destroy(gameObject);
    }
}
