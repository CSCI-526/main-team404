using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectToGenerate;
    public float timeInterval;
    private float timer;
    private GameObject generatedObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generatedObject == null)
        {
            timer -= Time.deltaTime;
        }
        
        if (timer <= 0)
        {
            generatedObject = Instantiate(objectToGenerate, transform.position, Quaternion.identity);
            timer = timeInterval;
        }
    }
}
