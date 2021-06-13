using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Enemy
{

    private Vector3 b_move;
    private float b_movementSpeed = 4f;
    protected static new int damagePoints = 30;
    private Rigidbody b_rigidbody;

    public GameObject target;

    Vector3 heading;

    void Awake()
    {
        b_rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {

        heading = target.transform.position - transform.position;
        //Movement 
        b_move = (heading).normalized;
        transform.position += b_move * b_movementSpeed * Time.deltaTime;

    }


    public override int GetDamagePoints()
    {
        return damagePoints;
    }


}
