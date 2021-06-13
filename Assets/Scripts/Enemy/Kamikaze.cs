using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Enemy
{
    [SerializeField] private int m_scoreValue = 10;

    private Vector3 b_move;
    private float b_movementSpeed = 7.0f;
    protected static new int damagePoints = 30;
    private Rigidbody b_rigidbody;

    Vector3 heading;

    void Awake()
    {
        b_rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * b_movementSpeed * Time.deltaTime;
    }


    public override int GetDamagePoints()
    {
        return damagePoints;
    }

    public override void Die()
    {
        base.Die();

        AddScore(m_scoreValue);
    }

}
