using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fattie : MonoBehaviour
{

    private Projectile f_projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Shoot()
    {
        f_projectile = Instantiate(p_projectileGameObject, transform.position, transform.rotation).GetComponent<FattieProjectile>();
        f_projectile.SetDirection(GetFireDirection());
    }

}
