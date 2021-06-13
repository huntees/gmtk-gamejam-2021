﻿using UnityEngine;
using TMPro;

public class HudManager : MonoBehaviour
{
    private PlayerController m_playerController;
    private SpawnManager m_spawnManager;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI m_waveText;
    [SerializeField] private TextMeshProUGUI m_scoreValueText;
    [SerializeField] private TextMeshProUGUI m_ammoValueText;
    [SerializeField] private GameObject[] m_lifeIcons;

    // Start is called before the first frame update
    void Awake()
    {
        m_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        m_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        m_playerController.HUD_updateAmmo += UpdateAmmoText;
        m_playerController.HUD_restoreLife += RestoreLife;
        m_playerController.HUD_removeLife += RemoveLife;

        m_spawnManager.HUD_updateWave += UpdateWaveText;
        m_spawnManager.HUD_updateScore += UpdateScoreText;
    }

    void UpdateWaveText(int wave)
    {
        m_waveText.text = "Wave: " + wave;
    }

    void UpdateScoreText(int score)
    {
        m_scoreValueText.text = score.ToString();
    }

    void UpdateAmmoText(int ammo)
    {
        m_ammoValueText.text = "x" + ammo;
    }

    void RestoreLife()
    {
        for(int i = 0; i < m_lifeIcons.Length; i++)
        {
            if(!m_lifeIcons[i].activeInHierarchy)
            {
                m_lifeIcons[i].SetActive(true);
                break;
            }
        }
    }

    void RemoveLife()
    {
        for (int i = 0; i < m_lifeIcons.Length; i++)
        {
            if (m_lifeIcons[i].activeInHierarchy)
            {
                m_lifeIcons[i].SetActive(false);
                break;
            }
        }
    }
}