using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    [SerializeField] Text timerText;
    [SerializeField] Image timerCircle;

    public float durationMinute, durationSecond, totalDuration;
    float currentTime;

    IEnumerator UT, ST;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {

        }
    }

    private void OnEnable()
    {
        Debug.Log("Enabled Timer");
        UT = UpdateTimer();
        ST = StopTimerCase();
        SetDuration(0f, 0f);
        SetTimerText();
        //StartCoroutine(UT);
    }

    public void StartTimer()
    {
        StopCoroutine(ST);
        if (!timerCircle.gameObject.activeInHierarchy) timerCircle.gameObject.SetActive(true);
        if (!timerText.gameObject.activeInHierarchy) timerText.gameObject.SetActive(true);
        StartCoroutine(UT);
    }
    public void StopTimer()
    {
        StartCoroutine(ST);
        StopCoroutine(UT);
    }
    public void SetDuration(float min, float sec)
    {
        durationMinute = min;
        durationSecond = sec;
        totalDuration = GetDuration();
        currentTime = totalDuration;
        timerCircle.fillAmount = 1f;
    }
    public float GetDuration()
    {
        return (durationMinute * 60f) + durationSecond;
    }

    void SetTimerText()
    {
        if (durationSecond < 10)
        {
            timerText.text = "0" + durationMinute.ToString() + ":0" + durationSecond.ToString();
        }
        else
        {
            timerText.text = "0" + durationMinute.ToString() + ":" + durationSecond.ToString();
        }

    }
    void SetCircle()
    {
        float setValue;
        currentTime -= 1;
        setValue = currentTime / totalDuration;
        timerCircle.fillAmount = setValue;
    }

    IEnumerator UpdateTimer()
    {
        while (durationMinute >= 0)
        {
            yield return new WaitForSeconds(1f);
            if (durationSecond == 0)
            {
                if (durationMinute == 0)
                {
                    Level1Manager.Instance.TimesUpProcess();
                    break;
                }
                durationMinute -= 1;
                durationSecond = 59f;
            }
            else if (durationSecond > 0)
            {
                durationSecond -= 1;
            }
            if(durationSecond == 10 && durationMinute == 0)
            {
                FindObjectOfType<AudioManager>().Play("Last10SecondsSound");
            }
            SetTimerText();
            SetCircle();
        }
        SetTimerText();
        SetCircle();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator StopTimerCase()
    {
        while (true)
        {
            timerCircle.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            timerCircle.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
