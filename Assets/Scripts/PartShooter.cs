using UnityEngine;

public class PartShooter : Part
{
    [SerializeField] private GameObject m_projectileGameObject;
    private Projectile m_projectile;

    [SerializeField] private float m_attackTime = 1.0f;
    private float m_nextAttackTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isConnected)
        {
            return;
        }

        if (Time.time >= m_nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                m_nextAttackTime = Time.time + m_attackTime;
            }
        }
    }

    private void Shoot()
    {
        m_projectile = Instantiate(m_projectileGameObject, transform.position, transform.rotation).GetComponent<Projectile>();
        m_projectile.SetDirection(GetFireDirection());
    }

    private Vector3 GetFireDirection()
    {
        return (transform.position - transform.parent.position).normalized;
    }
}
