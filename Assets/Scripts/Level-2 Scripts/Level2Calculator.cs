using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level2Calculator : MonoBehaviour
{
    public static Level2Calculator Instance;

    [SerializeField] GameObject firstStarObj, secondStarObj, thirdStarObj;
    [SerializeField] Text wrongSelectText, timeLeftText, scoreText, showColorCountLeftText, countdownText;
    bool firstStar, secondStar, thirdStar;
    public float Score = 0;
    public int wrongSelectCount = 0, showColorHintCount = 2;

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

        if(showColorHintCount > 0)
        {
            Score += showColorHintCount * 150f;
        }
    }
    void CalculateStars()
    {
        if (Level2Manager.Instance.gameEnded) firstStar = true;
        if (wrongSelectCount < 5) secondStar = true;
        if (Timer.Instance.GetDuration() > 59f) thirdStar = true;
    }
    public void SetEndGameText()
    {
        CalculateScore();
        CalculateStars();
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
        StartCoroutine(SetActiveStars());
    }

    IEnumerator SetActiveStars()
    {
        yield return new WaitForSeconds(1f);
        if (firstStar) firstStarObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (secondStar) secondStarObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (thirdStar) thirdStarObj.SetActive(true);
    }

    public IEnumerator ShowColorProcess()
    {
        if(showColorHintCount == 0)
        {
            showColorCountLeftText.color = Color.red;
            showColorCountLeftText.text = "No Left";
            yield return new WaitForSeconds(1f);
            showColorCountLeftText.color = Color.black;
        }
        else
        {
            Level2Manager.Instance.ShowColors();
            countdownText.text = "5";
            yield return new WaitForSeconds(1f);
            countdownText.text = "4";
            yield return new WaitForSeconds(1f);
            countdownText.text = "3";
            yield return new WaitForSeconds(1f);
            countdownText.text = "2";
            yield return new WaitForSeconds(1f);
            countdownText.text = "1";
            yield return new WaitForSeconds(1f);
            countdownText.text = " ";
            Level2Manager.Instance.HideColors();
            showColorHintCount--;
            showColorCountLeftText.text = "Left : " + showColorHintCount;
        }
    }
}
