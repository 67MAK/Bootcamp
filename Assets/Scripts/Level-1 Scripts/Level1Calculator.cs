using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Calculator : MonoBehaviour
{
    public static Level1Calculator Instance;

    [SerializeField] GameObject firstStarObj, secondStarObj, thirdStarObj;
    [SerializeField] Text wrongSelectText, timeLeftText, scoreText;
    bool firstStar, secondStar, thirdStar;
    public float Score = 0;
    public int wrongSelectCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CalculateScore()
    {
        Score += Timer.Instance.GetDuration() * 10;
        if(wrongSelectCount == 0)
        {
            Score += 100f;
        }
        else if (wrongSelectCount > 3)
        {
            if (Score > 60f) Score -= 60f;
            else if (Score <= 60f) Score = 0f;
        }
        else if (wrongSelectCount > 15)
        {
            if (Score > 120f) Score -= 120f;
            else if (Score <= 120f) Score = 0f;
        }
    }
    void CalculateStars()
    {
        if (Level1Manager.Instance.gameEnded) firstStar = true;
        if (wrongSelectCount < 3) secondStar = true;
        if (Timer.Instance.GetDuration() > 20f) thirdStar = true;
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
        DataManager.Instance.Level1Score = Score;
        DataManager.Instance.SaveData();
        StartCoroutine(SetActiveStars());
    }

    IEnumerator SetActiveStars()
    {
        Debug.Log("firstStar : " + firstStar);
        Debug.Log("secondStar : " + secondStar);
        Debug.Log("thirdStar : " + thirdStar);
        yield return new WaitForSeconds(1f);
        if (firstStar) firstStarObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (secondStar) secondStarObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (thirdStar) thirdStarObj.SetActive(true);
    }
}
