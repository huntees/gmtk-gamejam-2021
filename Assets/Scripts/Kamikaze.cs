using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{

    private Vector3 b_move;
    private float b_movementSpeed = 2.5f;
    private int damagePoints = 25;
    private Rigidbody b_rigidbody;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        b_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { }

    void FixedUpdate()
    {

        var heading = target.transform.position - transform.position;
        //Movement 
        b_move = (heading).normalized;
        transform.position += b_move * b_movementSpeed * Time.deltaTime;

    }

    public int GetDamagePoints()
    {
        return damagePoints;
    }

}
