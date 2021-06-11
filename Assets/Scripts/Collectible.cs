using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameObject collidedObject;

    private bool m_isConnected = false;

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
        if (!m_isConnected)
        {
            collidedObject = collision.gameObject;

            if (collidedObject.CompareTag("Player"))
            {
                transform.parent = collidedObject.transform;
                m_isConnected = true;
            }
        }
    }
}
