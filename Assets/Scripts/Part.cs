using UnityEngine;

public class Part : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private GameObject m_collidedObject;

    protected bool m_isConnected = false;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(transform.parent == null)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Eject();
        }
    }

    private void Eject()
    {
        //turn physics on again and fire part away from player
        m_rigidbody.isKinematic = false;
        m_rigidbody.AddForce(GetFireDirection() * 2.0f, ForceMode.Impulse);

        //remove parent from part
        transform.parent = null;
        transform.tag = "Part";

        //Re-enable the ability to be connected after a delay, otherwise part connects immediately after detaching
        Invoke("EnableConnect", 1.5f);
    }

    private void EnableConnect()
    {
        m_isConnected = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!m_isConnected)
        {
            m_collidedObject = collision.gameObject;

            if (m_collidedObject.CompareTag("Player"))
            {
                m_isConnected = true;
                transform.parent = m_collidedObject.transform;
                transform.tag = "Player";

                m_rigidbody.velocity = Vector3.zero;
                m_rigidbody.isKinematic = true;
            }
        }
    }

    protected Vector3 GetFireDirection()
    {
        return (transform.position - transform.parent.position).normalized;
    }
}
