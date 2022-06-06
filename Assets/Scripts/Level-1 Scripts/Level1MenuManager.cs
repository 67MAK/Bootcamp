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
        if (!Level1Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
            Level1Manager.Instance.PauseGameProcess();
        }
    }

    public void MainMenuButton()
    {
        FindObjectOfType<AudioManager>().Stop("LivingInAMadWorld");
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ContinueButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Level1Manager.Instance.pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Level1Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void NextLevelButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
