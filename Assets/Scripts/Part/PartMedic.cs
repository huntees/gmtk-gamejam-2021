using UnityEngine;

public class PartMedic : Part
{
    private int medicStock = 1;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (m_collidedObject.CompareTag("Player"))
        {
            m_playerController.RestoreHealth();
            medicStock = 0;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (transform.parent == null)
        {
            return;
        }
        base.Update();
    }

    public int GetMedicStock() { return medicStock; }

}