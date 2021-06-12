using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected int damagePoints = 15;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 2.3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual int GetDamagePoints()
    {
        return damagePoints;
    }

    public void Die()
    {
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().DecreaseEnemyCount();
        Destroy(gameObject);
    }
}
