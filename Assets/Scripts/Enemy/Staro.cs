using UnityEngine;

public class Staro : Enemy
{

    [SerializeField] private GameObject[] m_barrels;

    private FattieProjectile m_projectile;
    [SerializeField] private GameObject m_projectileGameObject;

    [SerializeField] private float m_attackTime = 1.0f;
    private float m_nextAttackTime = 5.0f;
    protected static new int damagePoints = 20;

    private float rotationSpeed = 1.0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= m_nextAttackTime)
        {
            Shoot();
            Invoke("Shoot", 0.8f);
            Invoke("Shoot", 1.6f);
            Invoke("Shoot", 2.4f);

            m_nextAttackTime = Time.time + m_attackTime;
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 5.0f);
    }

    private void Shoot()
    {
        for(int i = 0; i < m_barrels.Length; i++)
        {
            m_projectile = Instantiate(m_projectileGameObject, m_barrels[i].transform.position, m_barrels[i].transform.rotation).GetComponent<FattieProjectile>();
            m_projectile.SetDirection(m_barrels[i].transform.forward);
        }
    }

    public override int GetDamagePoints()
    {
        return damagePoints;
    }


}
