using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fattie : Enemy
{

    private FattieProjectile f_projectile;
    [SerializeField] private GameObject f_projectileGameObject;

    [SerializeField] private float f_attackTime = 1.0f;
    private float f_nextAttackTime = 5.0f;
    protected static new int damagePoints = 20;
    public GameObject target;

    private float rotationSpeed = 1.0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // How does the Fattie move?... Get to a certain distance to the player and then rotate around

        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        var str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);


        if (Time.time >= f_nextAttackTime)
        {
            Shoot();
            f_nextAttackTime = Time.time + f_attackTime;
        }
    }

    private void Shoot()
    {
        // if we instantiate the FattieProjectile right at its position, it's going crazy
        // wrong, should depend of the positions...
        // Vector3 offsetStart = new Vector3(0, 0, 2);

        var dirPosOffset = (target.transform.position - transform.position).normalized;
        // var offsetStart = 

        f_projectile = Instantiate(f_projectileGameObject, transform.position + dirPosOffset, transform.rotation).GetComponent<FattieProjectile>();
        f_projectile.SetDirection(f_projectile.GetFireDirection(transform));

    }

    public override int GetDamagePoints()
    {
        return damagePoints;
    }


}
