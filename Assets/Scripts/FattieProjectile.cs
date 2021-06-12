using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattieProjectile : MonoBehaviour
{

    [SerializeField] private float f_movementSpeed = 5.0f;
    [SerializeField] private int f_health = 50;
    private Vector3 f_move;
    private Vector3 f_direction = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {

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


    public Vector3 GetFireDirection(UnityEngine.Transform shooterTransform)
    {
        return (transform.position - shooterTransform.position).normalized;
    }

}
