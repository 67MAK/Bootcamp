using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2MenuManager : MonoBehaviour
{
    [SerializeField] GameObject muteButton;
    Image muteButtonImg;
    [SerializeField] Sprite[] switchSprite;
    int i = 0;

    private void Start()
    {
        muteButtonImg = muteButton.GetComponent<Button>().image;
    }
    public void PauseButton()
    {
        if (!Level2Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
            Level2Manager.Instance.PauseGameProcess();
        }
    }
    public void MuteMusicButton()
    {
        if (i == 0)
        {
            FindObjectOfType<AudioManager>().Stop("AThirstyRoseInTheDesert");
            muteButtonImg.sprite = switchSprite[i];
            i = 1;
        }
        else if (i == 1)
        {
            FindObjectOfType<AudioManager>().Play("AThirstyRoseInTheDesert");
            muteButtonImg.sprite = switchSprite[i];
            i = 0;
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
