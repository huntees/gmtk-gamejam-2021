using UnityEngine;

public class PartShooter : Part
{
    private GameObject m_projectileGameObject;
    private ObjectPool m_ammoObjectPool;
    private Projectile m_projectile;

    [SerializeField] private float m_attackTime = 1.0f;
    private float m_nextAttackTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_ammoObjectPool = GameObject.Find("AmmoObjectPool").GetComponent<ObjectPool>();
    }

    // protected override string GetKind()
    // {
    //     return "shooter";
    // }

    // Update is called once per frame
    protected override void Update()
    {
        if (transform.parent == null)
        {
            return;
        }

        base.Update();

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
        if (m_playerController.GetAmmo() > 0)
        {
            // Reduce the number of ammo
            m_playerController.ReduceAmmo(1); // we might consider big missiles or something...

            m_projectileGameObject = m_ammoObjectPool.GetAvailableObject();
            m_projectileGameObject.transform.position = transform.position;

            m_projectile = m_projectileGameObject.GetComponent<Projectile>();
            m_projectile.SetDirection(GetFireDirection());

            m_projectileGameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No ammo!");
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!m_isConnected)
        {
            base.OnCollisionEnter(collision);

            if (m_collidedObject.CompareTag("Player"))
            {
                transform.rotation = Quaternion.LookRotation((transform.position - transform.parent.position).normalized);
            }
        }
    }
}
