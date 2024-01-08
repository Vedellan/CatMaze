using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public PauseManager pauseManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            pauseManager = GameObject.Find("Pause Manager").GetComponent<PauseManager>();
        }
    }

    #region Load
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadScene(sceneName);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion Load


    #region Pause
    public void PauseGame()
    {
        pauseManager.PauseGame();
    }

    public void ResumeGame()
    {
        pauseManager.ResumeGame();
    }
    #endregion Pause
}
