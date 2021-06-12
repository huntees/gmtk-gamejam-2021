using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattieProjectile : MonoBehaviour
{

    [SerializeField] private float f_movementSpeed = 2.0f;
    [SerializeField] private int f_health = 50;
    private Vector3 f_move;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    protected Vector3 GetFireDirection()
    {
        return (transform.position - transform.parent.position).normalized;
    }

}
