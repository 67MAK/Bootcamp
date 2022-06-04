using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3MenuManager : MonoBehaviour
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
        if (!Level3Manager.Instance.gameEnded)
        {
            Level3Manager.Instance.PauseGameProcess();
        }
    }

    public void MainMenuButton()
    {
        Debug.Log("Main Menu...!");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ContinueButton()
    {
        Level3Manager.Instance.pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Level3Manager.Instance.Invoke("SetCanSelect", 0.5f);
    }
    public void ShowColorsButton()
    {
        Debug.Log("Cubes Hiding : " + Level3Manager.Instance.isColorHiding);
        if (Level3Manager.Instance.isColorHiding && !Level3Manager.Instance.gameEnded)
        {
            StartCoroutine(Level3Calculator.Instance.ShowColorProcess());
        }
    }
}
