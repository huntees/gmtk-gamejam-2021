using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int m_health = 100;
    private int m_currentHealth = 100;

    [SerializeField] private int m_ammoCount = 50;

    [SerializeField] private float m_movementSpeed = 10.0f;
    [SerializeField] private float m_currentMovementSpeed = 10.0f;
    [SerializeField] private float m_turnRate = 100.0f;

    private Vector3 m_move;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 145;
        m_currentHealth = m_health;
    }

    void FixedUpdate()
    {
        transform.Rotate(Input.GetAxisRaw("Rotate") * Vector3.up * m_turnRate * Time.deltaTime);

        //Movement 
        m_move = (Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right).normalized;
        transform.position += m_move * m_currentMovementSpeed * Time.deltaTime;
    }

    public void TakeDamage(int numDamage)
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

        // todo update: all enemies should inherit an Enemy class (including projectiles should have a GetDamagePoints function)
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("contact with an ennemy");
            int damageToSet = collider.GetComponent<Enemy>().GetDamagePoints();
            Debug.Log("damageToSet: " + damageToSet);
            TakeDamage(damageToSet);
            Destroy(collider);

        }
        //else if (collider.CompareTag("Part"))
        //{
        //    // Check what part we hit and adapt accordingly.
        //    // string type = collider.GetComponent<Part>().GetKind();
        //    Debug.Log("collided with a Part");
        //    var typeCollider = collider.GetType();
        //    if (typeCollider == typeof(PartAmmo))
        //    {
        //        numberAmmo += collider.GetComponent<PartAmmo>().GetAmmoStock();
        //    }
        //}

    }

    // First version, could be improved
    public void AddAmmo(int newAmmo)
    {
        m_ammoCount += newAmmo;
    }
    public int GetAmmo()
    {
        return m_ammoCount;
    }

    public void ReduceAmmo(int cost)
    {
        m_ammoCount -= cost;
    }

    public void UpdateMovementSpeed()
    {
        // Speed variations: dependent of number of children. Should never reach 0, but can get ridiculously low. (to adapt based on types of children, but maybe not this section of the code)
        //m_movementSpeed = 1.0f + (11.0f / Mathf.Sqrt(transform.hierarchyCount));
        var childCount = transform.hierarchyCount - 2;
        m_currentMovementSpeed = m_movementSpeed - ((childCount / (childCount + 30.0f)) * 10.0f);
    }
}
