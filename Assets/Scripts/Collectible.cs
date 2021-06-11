using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameObject collidedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        collidedObject = collision.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            transform.parent = collidedObject.transform;
        }
    }
}
