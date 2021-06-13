using UnityEngine;

public class PartMedic : Part
{
    private bool m_collected = false;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (!m_collected && m_collidedObject.CompareTag("Player"))
        {
            m_playerController.RestoreHealth();
            m_collected = true;
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

}