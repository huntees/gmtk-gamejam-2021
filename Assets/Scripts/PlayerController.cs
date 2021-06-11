using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 5.0f;
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

        //Movement 
        m_move = (Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right).normalized;

        transform.position += m_move * m_movementSpeed * Time.deltaTime;
    }
}
