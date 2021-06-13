using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattieProjectile : Enemy
{

    [SerializeField] private float f_movementSpeed = 5.0f;
    [SerializeField] private int f_health = 50;
    private Vector3 f_move;
    private Vector3 f_direction = Vector3.zero;
    protected static new int damagePoints = 20;

    private GameObject m_collidedObject;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += f_direction * f_movementSpeed * Time.deltaTime;
    }

    public void SetDirection(Vector3 shootDirection)
    {
        f_direction = shootDirection;
    }

    public override int GetDamagePoints()
    {
        return damagePoints;
    }

    // How long until we destroy it? 10s for now.
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_collidedObject = other.gameObject;

        if (m_collidedObject.tag == "Player") {

            var playerController = m_collidedObject.GetComponent<PlayerController>();
            var partController = m_collidedObject.GetComponent<Part>();

            if(playerController != null)
            {
                playerController.TakeDamage(GetDamagePoints());
            } 
            else if(partController != null)
            {
                partController.EjectOnHit();
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
