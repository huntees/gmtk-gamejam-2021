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

    private float rotationSpeed = 1.0f;


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
        f_projectile.SetDirection(f_projectile.GetFireDirection(transform));

    }

    public override int GetDamagePoints()
    {
        return damagePoints;
    }


}
