using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level2Calculator : MonoBehaviour
{
    public static Level2Calculator Instance;

    [SerializeField] Text wrongSelectText, timeLeftText, scoreText;
    public float Score = 0;
    public int wrongSelectCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void CalculateScore()
    {
        Score += Timer.Instance.GetDuration() * 10;
        if (wrongSelectCount == 0)
        {
            Score += 1000f;
        }
        else if (wrongSelectCount > 10)
        {
            if (Score > 60f) Score -= 60f;
            else if (Score <= 60f) Score = 0f;
        }
        else if (wrongSelectCount > 50)
        {
            if (Score > 120f) Score -= 120f;
            else if (Score <= 120f) Score = 0f;
        }
    }
    public void SetEndGameText()
    {
        CalculateScore();
        wrongSelectText.text = "Wrong Selections : " + wrongSelectCount;
        if (Timer.Instance.durationSecond > 10)
        {
            timeLeftText.text = "Time Left : 0" + Timer.Instance.durationMinute + ":" + Timer.Instance.durationSecond;
        }
        else if (Timer.Instance.durationSecond < 10)
        {
            timeLeftText.text = "Time Left : 0" + Timer.Instance.durationMinute + ":0" + Timer.Instance.durationSecond;
        }
        scoreText.text = "Total Score : " + Score;
    }
}
