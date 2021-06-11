using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private GameObject m_collidedObject;

    private bool m_isConnected = false;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

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
            m_collidedObject = collision.gameObject;

            if (m_collidedObject.CompareTag("Player"))
            {
                transform.parent = m_collidedObject.transform;
                m_isConnected = true;
                m_rigidbody.velocity = Vector3.zero;
                m_rigidbody.isKinematic = true;
            }
        }
    }
}
