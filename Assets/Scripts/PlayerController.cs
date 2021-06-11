using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 m_move;
    private float m_movementSpeed = 5.0f;
    private int lifePoints = 100;


    // Start is called before the first frame update
    void Start()
    {

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
