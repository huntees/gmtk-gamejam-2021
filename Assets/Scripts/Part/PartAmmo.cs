using UnityEngine;

public class PartAmmo : Part
{
    private int stockAmmo = 100;

    // Start is called before the first frame update
    void Start() { }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (m_collidedObject.CompareTag("Player"))
        {
            m_playerController.AddAmmo(stockAmmo);
            stockAmmo = 0;
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

    public int GetAmmoStock() { return stockAmmo; }

}