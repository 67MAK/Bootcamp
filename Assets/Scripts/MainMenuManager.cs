using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject highScoreScreen, levelsScreen, titleText, muteButton;
    Text text;
    Image muteButtonImg;
    [SerializeField] Sprite[] switchSprite;
    int i = 0;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Exhale");
        text = titleText.GetComponent<Text>();
        muteButtonImg = muteButton.GetComponent<Button>().image;
        StartCoroutine(colorRain());
    }

    IEnumerator colorRain()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            text.color = new Color(1f, 0f, 0.784f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0.078f, 0f, 1f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(1f, 1f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0.078f, 1f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0.5f, 0.28f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(1f, 0f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0f, 1f, 1f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0f, 0f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(1f, 0.431f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0f, 0.392f, 0.392f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0.392f, 0f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0.392f, 0f, 0.392f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0f, 0.392f, 0f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0f, 0f, 0.431f);
            yield return new WaitForSeconds(1f);
            text.color = new Color(0.353f, 0.353f, 0.353f);
        }
    }

    public void PlayGameButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        levelsScreen.SetActive(true);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
    public void HighScoreButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        DataManager.Instance.LoadData();
        highScoreScreen.transform.GetChild(4).GetComponent<Text>().text = "Level 3 Highscore : " + DataManager.Instance.level3HighScore.ToString();
        highScoreScreen.transform.GetChild(3).GetComponent<Text>().text = "Level 2 Highscore : " + DataManager.Instance.level2HighScore.ToString();
        highScoreScreen.transform.GetChild(2).GetComponent<Text>().text = "Level 1 Highscore : " + DataManager.Instance.level1HighScore.ToString();
        highScoreScreen.SetActive(true);
    }
    public void CloseHighScoreButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        highScoreScreen.SetActive(false);
    }

    public void CloseLevelsScreen()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        levelsScreen.SetActive(false);
    }

    public void MuteMusicButton()
    {
        if(i == 0)
        {
            FindObjectOfType<AudioManager>().Stop("Exhale");
            muteButtonImg.sprite = switchSprite[i];
            i = 1;
        }
        else if(i == 1)
        {
            FindObjectOfType<AudioManager>().Play("Exhale");
            muteButtonImg.sprite = switchSprite[i];
            i = 0;
        }
    }

    public void Level1Button()
    {
        FindObjectOfType<AudioManager>().Stop("Exhale");
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }
    public void Level2Button()
    {
        FindObjectOfType<AudioManager>().Stop("Exhale");
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(2);
        Time.timeScale = 1.0f;
    }
    public void Level3Button()
    {
        FindObjectOfType<AudioManager>().Stop("Exhale");
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(3);
        Time.timeScale = 1.0f;
    }

    public void ResetLevel1DataButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        DataManager.Instance.level1HighScore = 0;
        highScoreScreen.transform.GetChild(2).GetComponent<Text>().text = "Level 1 Highscore : " + DataManager.Instance.level1HighScore.ToString();
        DataManager.Instance.SaveData();
    }
    public void ResetLevel2DataButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        DataManager.Instance.level2HighScore = 0;
        highScoreScreen.transform.GetChild(3).GetComponent<Text>().text = "Level 2 Highscore : " + DataManager.Instance.level2HighScore.ToString();
        DataManager.Instance.SaveData();
    }
    public void ResetLevel3DataButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        DataManager.Instance.level3HighScore = 0;
        highScoreScreen.transform.GetChild(4).GetComponent<Text>().text = "Level 3 Highscore : " + DataManager.Instance.level3HighScore.ToString();
        DataManager.Instance.SaveData();
    }
}
