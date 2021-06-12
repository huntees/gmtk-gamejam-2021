using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 12.0f;
    [SerializeField] private int m_health = 100;
    private int m_currentHealth = 100;

    private Vector3 m_move;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 145;
        m_currentHealth = m_health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        // Speed variations: dependent of number of children. Should never reach 0, but can get ridiculously low. (to adapt based on types of children, but maybe not this section of the code)
        m_movementSpeed = 1.0f + (11.0f / Mathf.Sqrt(transform.hierarchyCount));

        //Movement 
        m_move = (Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right).normalized;
        transform.position += m_move * m_movementSpeed * Time.deltaTime;
    }

    void TakeDamage(int numDamage)
    {
        m_currentHealth -= numDamage;
        m_health -= numDamage;
        if (m_health <= 0) { Die(); }
    }

    void Die()
    {
        Debug.Log("You died!");
    }


    void OnCollisionEnter(Collision collision)
    {
        var collider = collision.gameObject;

        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("contact with an ennemy");
            int damageToSet = collider.GetComponent<Kamikaze>().GetDamagePoints();
            TakeDamage(damageToSet);
            Object.Destroy(collider);

        }
    }


}
