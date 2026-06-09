using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{

    public GameObject ennemyPreFab;
    public float cooldown;
    public float spawnMin, spawnMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            Vector3 spawnPos = transform.position + Vector3.right * Random.Range(spawnMin, spawnMax);

        Instantiate(ennemyPreFab, spawnPos);
    }

    private void Instantiate(GameObject ennemyPreFab, Vector3 spawnPos)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
