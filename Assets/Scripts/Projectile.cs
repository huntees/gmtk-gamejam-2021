using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_projectileSpeed = 10.0f;
    private Vector3 m_direction = Vector3.zero;

    private GameObject m_collidedObject;

    void OnEnable()
    {
        //Destroy itself after 10 seconds
        Invoke("DisableObject", 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_direction * m_projectileSpeed * Time.deltaTime;
    }

    private void DisableObject()
    {
        CancelInvoke();
        gameObject.SetActive(false);
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
            DisableObject();
            m_collidedObject.gameObject.GetComponent<Enemy>().Die();
        }
    }
}
