using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        // Locks it so only the Y axis moves
        transform.LookAt(targetPosition);
    }
}