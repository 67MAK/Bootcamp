using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2MenuManager : MonoBehaviour
{
    public void PauseButton()
    {
        if (!Level2Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
            Level2Manager.Instance.PauseGameProcess();
        }
    }

    public void MainMenuButton()
    {
        FindObjectOfType<AudioManager>().Stop("AThirstyRoseInTheDesert");
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
        Level2Manager.Instance.pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Level2Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        if (Level2Manager.Instance.isColorHiding && !Level2Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
            StartCoroutine(Level2Calculator.Instance.ShowColorProcess());
        }
    }
}
