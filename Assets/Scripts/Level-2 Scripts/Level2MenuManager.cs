using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()
    {
        if (!Level2Manager.Instance.gameEnded)
        {
            Level2Manager.Instance.PauseGameProcess();
        }
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ContinueButton()
    {
        Level2Manager.Instance.pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Level2Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        if (Level2Manager.Instance.isColorHiding && !Level2Manager.Instance.gameEnded)
        {
            StartCoroutine(Level2Calculator.Instance.ShowColorProcess());
        }
    }
}
