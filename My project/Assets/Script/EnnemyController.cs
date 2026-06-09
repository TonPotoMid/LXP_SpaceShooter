using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    float speed = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.back * speed * Time.deltaTime;
    }
}
