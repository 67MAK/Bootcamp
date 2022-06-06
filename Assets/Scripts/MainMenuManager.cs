using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject highScoreScreen, levelsScreen, titleText;
    MeshRenderer _renderer;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Exhale");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        levelsScreen.SetActive(true);
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
