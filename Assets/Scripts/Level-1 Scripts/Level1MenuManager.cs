using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level1MenuManager : MonoBehaviour
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
        if (!Level1Manager.Instance.gameEnded)
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
            Level1Manager.Instance.PauseGameProcess();
        }
    }
    public void MuteMusicButton()
    {
        if (i == 0)
        {
            FindObjectOfType<AudioManager>().Stop("LivingInAMadWorld");
            muteButtonImg.sprite = switchSprite[i];
            i = 1;
        }
        else if (i == 1)
        {
            FindObjectOfType<AudioManager>().Play("LivingInAMadWorld");
            muteButtonImg.sprite = switchSprite[i];
            i = 0;
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
