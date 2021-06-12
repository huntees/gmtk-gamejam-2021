using UnityEngine;

public class Projectile : Enemy
{
    [SerializeField] private float m_projectileSpeed = 10.0f;
    private Vector3 m_direction = Vector3.zero;

    private GameObject m_collidedObject;

    void Start()
    {
        //Destroy itself after 10 seconds
        Invoke("SelfDestruct", 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_direction * m_projectileSpeed * Time.deltaTime;
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 shootDirection)
    {
        m_direction = shootDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_collidedObject = other.gameObject;

        if (m_collidedObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(m_collidedObject);
        }
    }
}
