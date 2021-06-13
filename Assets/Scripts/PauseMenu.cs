using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_pauseMenuObject;
    [SerializeField] private GameObject m_gameOverObject;

    private bool m_isPaused = false;
    private bool m_isGameOver = false;

    public void TriggerGameOver()
    {
        m_isGameOver = true;

        gameObject.SetActive(true);
        m_pauseMenuObject.SetActive(false);
        m_gameOverObject.SetActive(true);

        //Pause game simulation after 1.7 seconds
        Invoke("PauseSim", 1.7f);
    }

    public void TogglePauseMenu()
    {
        if (m_isGameOver)
        {
            return;
        }

        m_isPaused = !m_isPaused;

        if (m_isPaused)
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }

    private void ContinueGame()
    {
        gameObject.SetActive(false);
        ResumeSim();
    }

    private void PauseGame()
    {
        gameObject.SetActive(true);
        PauseSim();
    }

    public void Restart()
    {
        ResumeSim();
        SceneManager.LoadScene(1);
    }

    public void ExitToMainMenu()
    {
        ResumeSim();
        SceneManager.LoadScene(0);
    }

    private void PauseSim()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeSim()
    {
        Time.timeScale = 1.0f;
    }

}
