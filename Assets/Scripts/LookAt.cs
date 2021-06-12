using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private GameObject player;
    public Transform target; // The object to look at

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform;
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        // Locks it so only the Y axis moves
        transform.LookAt(targetPosition);
    }
}