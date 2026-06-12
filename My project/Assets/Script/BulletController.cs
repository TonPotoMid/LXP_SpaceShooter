using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ennemy")
        {
            Destroy(collision.gameObject);
        }
           
    }
}
