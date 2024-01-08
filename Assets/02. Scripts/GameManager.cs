using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject clearPanel;
    public GameObject gameOverPanel;

    float timer = 0;
    bool isCleared = false;
    TextMeshProUGUI clearTime;

    public void Awake()
    {
        clearPanel = GameObject.Find("Clear Panel");
        clearTime = GameObject.Find("Time Text").GetComponent<TextMeshProUGUI>();
        gameOverPanel = GameObject.Find("GameOver Panel");

        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if(isCleared == false)
        {
            timer += Time.deltaTime;
        }
    }

    public void GameClear()
    {
        clearTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)(timer / 3600), (int)((timer % 3600) / 60), (int)(timer % 60));
        clearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
