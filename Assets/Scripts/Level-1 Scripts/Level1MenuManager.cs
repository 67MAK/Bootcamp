using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level1MenuManager : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()
    {
        Level1Manager.Instance.canSelect = false;
        Time.timeScale = 0f;
        Level1Manager.Instance.pauseScreen.SetActive(true);
    }

    public void MainMenuButton()
    {

    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ContinueButton()
    {
        Level1Manager.Instance.pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Level1Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
