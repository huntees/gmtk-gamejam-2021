﻿using UnityEngine;

public class Part : MonoBehaviour
{
    protected PlayerController m_playerController;
    private Rigidbody m_rigidbody;
    protected GameObject m_collidedObject;

    protected bool m_isConnected = false;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // protected virtual string GetKind()
    // {
    //     return "base";
    // }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (transform.parent == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Eject();
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 2.3f, transform.position.z);
        transform.rotation = Quaternion.Euler(-90.0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void Eject()
    {
        if(!m_isConnected)
        {
            return ;
        }

        //turn physics on again and fire part away from player
        m_rigidbody.isKinematic = false;
        m_rigidbody.AddForce(GetFireDirection() * 2.0f, ForceMode.Impulse);

        //remove parent from part
        transform.parent = null;
        transform.tag = "Part";

        //Re-enable the ability to be connected after a delay, otherwise part connects immediately after detaching
        Invoke("EnableConnect", 0.2f);

        m_playerController?.UpdateMovementSpeed();
    }

    private void EnableConnect()
    {
        m_isConnected = false;
    }

    private void EjectOnHit()
    {
        Eject();

        foreach (Transform child in transform)
        {
            child.transform.GetComponent<Part>().EjectOnHit();
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        m_collidedObject = collision.gameObject;

        if (!m_isConnected)
        {
            if (m_collidedObject.CompareTag("Player"))
            {
                m_isConnected = true;
                transform.parent = m_collidedObject.transform;
                transform.tag = "Player";

                m_rigidbody.velocity = Vector3.zero;
                m_rigidbody.isKinematic = true;

                m_playerController = transform.root.GetComponent<PlayerController>();
                m_playerController?.UpdateMovementSpeed();
            }
        }
        else
        {
            if (m_collidedObject.CompareTag("Enemy"))
            {
                foreach (Transform child in transform)
                {
                    child.transform.GetComponent<Part>().EjectOnHit();
                }

                Destroy(gameObject);
                m_collidedObject.gameObject.GetComponent<Enemy>().Die();

                m_playerController?.UpdateMovementSpeed();
            }
        }
    }

    protected Vector3 GetFireDirection()
    {
        return (transform.position - transform.parent.position).normalized;
    }
}