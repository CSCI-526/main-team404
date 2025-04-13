using UnityEngine;

public class DPMissleGenerator : MonoBehaviour
{
    public GameObject missilePrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2f;


    private void Start()
    {
        InvokeRepeating(nameof(SpawnMissiles), spawnInterval, spawnInterval);
    }

    private void SpawnMissiles()
    {
        int[] selectedIndices = GetRandomIndices(4, spawnPoints.Length);
        foreach (int index in selectedIndices)
        {
            Instantiate(missilePrefab, spawnPoints[index].position, missilePrefab.transform.rotation);
        }
    }

    private int[] GetRandomIndices(int count, int max)
    {
        System.Collections.Generic.List<int> indices = new System.Collections.Generic.List<int>();
        while (indices.Count < count)
        {
            int randomIndex = Random.Range(0, max);
            if (!indices.Contains(randomIndex))
            {
                indices.Add(randomIndex);
            }
        }
        return indices.ToArray();
    }
}
