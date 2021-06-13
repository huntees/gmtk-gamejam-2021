﻿using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private AudioSource m_audioSource;

    [SerializeField] private int m_health = 3;
    private int m_currentHealth = 3;

    [SerializeField] private int m_ammoCount = 50;

    [SerializeField] private float m_movementSpeed = 14.0f;
    [SerializeField] private float m_currentMovementSpeed = 14.0f;
    [SerializeField] private float m_turnRate = 100.0f;

    private Vector3 m_move;

    public event Action<int> HUD_updateAmmo;
    public event Action HUD_restoreLife;
    public event Action HUD_removeLife;

    [SerializeField] private PauseMenu m_pauseMenu;

    public bool m_isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        Application.targetFrameRate = 145;
        m_currentHealth = m_health;

        HUD_updateAmmo?.Invoke(m_ammoCount);
    }

    void FixedUpdate()
    {
        if(m_isDead)
        {
            return ;
        }

        transform.Rotate(Input.GetAxisRaw("Rotate") * Vector3.up * m_turnRate * Time.deltaTime);

        //Movement 
        m_move = (Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right).normalized;
        transform.position += m_move * m_currentMovementSpeed * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_pauseMenu.TogglePauseMenu();
        }
    }

    public void TakeDamage(int numDamage)
    {
        m_currentHealth--;
        HUD_removeLife?.Invoke();

        if (m_currentHealth <= 0) { Die(); }
    }

    public void RestoreHealth()
    {
        if(m_currentHealth < 3)
        {
            m_currentHealth++;
            HUD_restoreLife?.Invoke();
        }
    }

    void Die()
    {
        m_isDead = true;
        m_audioSource.Play();
        m_pauseMenu.TriggerGameOver();
    }


    void OnCollisionEnter(Collision collision)
    {
        var collider = collision.gameObject;

        // todo update: all enemies should inherit an Enemy class (including projectiles should have a GetDamagePoints function)
        if (collider.CompareTag("Enemy"))
        {
            TakeDamage(1);
            collider.GetComponent<Enemy>().Die();
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
        HUD_updateAmmo?.Invoke(m_ammoCount);
    }
    public int GetAmmo()
    {
        return m_ammoCount;
    }

    public void ReduceAmmo(int cost)
    {
        m_ammoCount -= cost;
        HUD_updateAmmo?.Invoke(m_ammoCount);
    }

    public void UpdateMovementSpeed()
    {
        // Speed variations: dependent of number of children. Should never reach 0, but can get ridiculously low. (to adapt based on types of children, but maybe not this section of the code)
        //m_movementSpeed = 1.0f + (11.0f / Mathf.Sqrt(transform.hierarchyCount));
        var childCount = transform.hierarchyCount - 6;
        m_currentMovementSpeed = m_movementSpeed - ((childCount / (childCount + 30.0f)) * 10.0f);
    }
}
