using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3MenuManager : MonoBehaviour
{

    public void PauseButton()
    {
        if (!Level3Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
            Level3Manager.Instance.PauseGameProcess();
        }
    }

    public void MainMenuButton()
    {
        FindObjectOfType<AudioManager>().Stop("EternalFire");
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
        Level3Manager.Instance.pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Level3Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        if (Level3Manager.Instance.isColorHiding && !Level3Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
            StartCoroutine(Level3Calculator.Instance.ShowColorProcess());
        }
    }
}
