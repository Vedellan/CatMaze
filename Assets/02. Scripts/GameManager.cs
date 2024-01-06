using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject clearPanel;
    public GameObject gameOverPanel;

    public void Awake()
    {
        clearPanel = GameObject.Find("Clear Panel");
        gameOverPanel = GameObject.Find("GameOver Panel");

        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void GameClear()
    {
        clearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
