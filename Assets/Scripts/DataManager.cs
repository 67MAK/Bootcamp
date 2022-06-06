using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public float level1HighScore, level2HighScore, level3HighScore;
    private float level1Score, level2Score, level3Score;

    EasyFileSave myFile;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartProcess();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartProcess()
    {
        myFile = new EasyFileSave();
        LoadData();
    }

    public void SaveData()
    {
        if (level1Score > level1HighScore) level1HighScore = level1Score;
        if (level2Score > level2HighScore) level2HighScore = level2Score;
        if (level3Score > level3HighScore) level3HighScore = level3Score;

        myFile.Add("level1HighScore", level1HighScore);
        myFile.Add("level2HighScore", level2HighScore);
        myFile.Add("level3HighScore", level3HighScore);
    }

    public void LoadData()
    {
        if (myFile.Load())
        {
            level1HighScore = myFile.GetFloat("level1HighScore");
            level2HighScore = myFile.GetFloat("level2HighScore");
            level3HighScore = myFile.GetFloat("level3HighScore");
        }
    }

    public float Level1Score
    {
        get
        {
            return level1Score;
        }
        set
        {
            level1Score = value;
        }
    }
    public float Level2Score
    {
        get
        {
            return level2Score;
        }
        set
        {
            level2Score = value;
        }
    }
    public float Level3Score
    {
        get
        {
            return level3Score;
        }
        set
        {
            level3Score = value;
        }
    }
}
