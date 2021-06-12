using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fattie : MonoBehaviour
{

    private FattieProjectile f_projectile;
    [SerializeField] private GameObject f_projectileGameObject;

    [SerializeField] private float f_attackTime = 1.0f;
    private float f_nextAttackTime = 5.0f;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= f_nextAttackTime)
        {
            Shoot();
            f_nextAttackTime = Time.time + f_attackTime;
        }
    }

    private void Shoot()
    {
        // if we instantiate the FattieProjectile right at its position, it's going crazy
        Vector3 offsetStart = new Vector3(0, 0, 2);
        f_projectile = Instantiate(f_projectileGameObject, transform.position + offsetStart, transform.rotation).GetComponent<FattieProjectile>();
        f_projectile.SetDirection(f_projectile.GetFireDirection(transform));

    }

}
