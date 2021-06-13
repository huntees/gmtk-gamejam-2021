using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fattie : Enemy
{

    [SerializeField] private GameObject m_barrel;

    private FattieProjectile f_projectile;
    [SerializeField] private GameObject f_projectileGameObject;

    [SerializeField] private float f_attackTime = 1.0f;
    private float f_nextAttackTime = 5.0f;
    protected static new int damagePoints = 20;
    public GameObject target;
    Vector3 heading;
    public float midDistance = 25.0f;
    private Vector3 b_move;
    private float b_movementSpeed = 2.5f;



    private float rotationSpeed = 1.0f;


    void Start()
    {
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {

        heading = transform.forward;
        float sqrLen = heading.sqrMagnitude;

        if (sqrLen > midDistance * midDistance)
        {
            //Movement if further than midDistance
            b_move = (heading).normalized;
            transform.position += b_move * b_movementSpeed * Time.deltaTime;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // How does the Fattie move?... Get to a certain distance to the player and then rotate around

        if (Time.time >= f_nextAttackTime)
        {
            Shoot();
            f_nextAttackTime = Time.time + f_attackTime;
        }
    }

    private void Shoot()
    {

        f_projectile = Instantiate(f_projectileGameObject, m_barrel.transform.position, m_barrel.transform.rotation).GetComponent<FattieProjectile>();
        f_projectile.SetDirection(m_barrel.transform.forward);

    }

    public override int GetDamagePoints()
    {
        return damagePoints;
    }


}
