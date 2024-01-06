using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    public void Awake()
    {
        pausePanel = GameObject.Find("Pause Panel");

        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
