using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public void PauseGame()
    {
        GameObject pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        if (!pausePanel)
        {
            return;
        }
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
    }
    public void UnPauseGame()
    {

        Time.timeScale = 1.0f;
    }
}
