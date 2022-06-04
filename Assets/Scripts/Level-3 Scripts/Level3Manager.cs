using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public static Level3Manager Instance;
    [SerializeField]
    GameObject cubePrefab, timerObj, timesUpScreen, endGameScreen, guideText;
    [SerializeField] public GameObject pauseScreen;

    GameObject[] _colorCubes = new GameObject[42];
    GameObject[] _flippedCubes = new GameObject[42];
    GameObject[] _selectedCubes = new GameObject[3];


    public Color[] _colorsOfCubes = new Color[42];
    Color[] colors = new Color[14];

    List<int> indexList = new List<int>();
    int[] _selectedIndex = new int[3];

    bool[] isCubeColored = new bool[42];
    public bool isColorHiding, canSelect, gameEnded;

    Vector3 instantiateAnchor = Vector3.zero;

    int colorCubesCount = 0, selectedCount = 0;
    int rand;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        SetFalse();
        colors[0] = new Color(1f, 0f, 0.784f);
        colors[1] = new Color(0.078f, 0f, 1f);
        colors[2] = new Color(1f, 1f, 0f);
        colors[3] = new Color(0.078f, 1f, 0f);
        colors[4] = new Color(0.5f, 0.28f, 0f);
        colors[5] = new Color(1f, 0f, 0f);
        colors[6] = new Color(0f, 1f, 1f);
        colors[7] = new Color(0f, 0f, 0f);
        colors[8] = new Color(1f, 0.431f, 0f);
        colors[9] = new Color(0f, 0.392f, 0.392f);
        colors[10] = new Color(0.392f, 0f, 0f);
        colors[11] = new Color(0.392f, 0f, 0.392f);
        colors[12] = new Color(0f, 0.392f, 0f);
        colors[13] = new Color(0f, 0f, 0.431f);
        StartCoroutine(CreateCubes());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowColors();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HideColors();
        }
    }

    IEnumerator CreateCubes()
    {
        canSelect = false;
        for (int i = 0; i < 42; i++)
        {
            _colorCubes[i] = Instantiate(cubePrefab, instantiateAnchor, Quaternion.identity) as GameObject;
            _colorCubes[i].name = "ColorCube" + i;
            _colorCubes[i].GetComponent<Level3MouseFeedback>()._index = i;
            indexList.Add(i);
            colorCubesCount++;
            instantiateAnchor = instantiateAnchor + new Vector3(1.25f, 0, 0);
            if ((i + 1) % 6 == 0)
            {
                instantiateAnchor = new Vector3(0, 0, instantiateAnchor.z);
                instantiateAnchor = instantiateAnchor + new Vector3(0, 0, 1.25f);
            }
            yield return new WaitForSeconds(0.15f);
        }
        instantiateAnchor = Vector3.zero;
        StartCoroutine(SetColors());
    }
    IEnumerator SetColors()
    {
        int setIndex, k = 0, colorIndex = 0;
        Debug.Log(indexList.Count);
        while (indexList.Count > 0)
        {
            rand = Random.Range(0, (indexList.Count - 1));
            setIndex = indexList[rand];
            if (!isCubeColored[setIndex])
            {
                _colorCubes[setIndex].GetComponent<MeshRenderer>().material.color = colors[colorIndex];
                _colorsOfCubes[setIndex] = colors[colorIndex];
                isCubeColored[setIndex] = true;
                indexList.RemoveAt(rand);
                k++;
                if (k == 3)
                {
                    k = 0;
                    colorIndex++;
                }
            }
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(2f);
        HideColors();
        guideText.SetActive(false);
        timerObj.SetActive(true);
        Timer.Instance.SetDuration(2f, 0f);
        Timer.Instance.StartTimer();
    }

    public void HideColors()
    {
        for (int i = 0; i < isCubeColored.Length; i++)
        {
            if (isCubeColored[i] && _colorCubes[i] != null)
            {
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f);
            }
        }
        isColorHiding = true;
        SetCanSelect();

    }
    void HideSelectedColors()
    {
        foreach(GameObject obj in _selectedCubes)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
    public void ShowColors()
    {
        isColorHiding = false;
        canSelect = false;
        for (int i = 0; i < _colorCubes.Length; i++)
        {
            if (isCubeColored[i] && _colorCubes[i] != null)
            {
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = _colorsOfCubes[i];
            }
        }
    }

    public void CubeSelect(int selectedIndex)
    {
        if (selectedCount == 0)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount++;
        }
        else if (selectedCount == 1 && _selectedCubes[0].GetComponent<Level3MouseFeedback>()._index != selectedIndex)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount++;
        }
        else if(selectedCount == 2 && _selectedCubes[1].GetComponent<Level3MouseFeedback>()._index != selectedIndex && _selectedCubes[0].GetComponent<Level3MouseFeedback>()._index != selectedIndex)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount = 0;
            canSelect = false;
            CheckColor();
        }
    }
    void CheckColor()
    {
        bool areColorsMatch = true;

        if(_selectedCubes[0].GetComponent<MeshRenderer>().material.color != _selectedCubes[1].GetComponent<MeshRenderer>().material.color)
            areColorsMatch = false;
        if(_selectedCubes[0].GetComponent<MeshRenderer>().material.color != _selectedCubes[2].GetComponent<MeshRenderer>().material.color)
            areColorsMatch = false;
        if(_selectedCubes[1].GetComponent<MeshRenderer>().material.color != _selectedCubes[2].GetComponent<MeshRenderer>().material.color)
            areColorsMatch = false;

        if (areColorsMatch)
        {
            MatchCorrect();
        }
        else
        {
            MatchWrong();
        }
    }

    void MatchCorrect()
    {
        Debug.Log("Match Correct");
        Level3Calculator.Instance.Score += 50f;
        StartCoroutine(FlipSelectedCubes());

        for(int i = 0; i < 3; i++)
        {
            _flippedCubes[_selectedIndex[i]] = _colorCubes[_selectedIndex[i]];
            _flippedCubes[_selectedIndex[i]].GetComponent<Level3MouseFeedback>().isFlipped = true;
        }

        _colorCubes[_selectedIndex[0]] = null;
        _colorCubes[_selectedIndex[1]] = null;
        _colorCubes[_selectedIndex[2]] = null;
        colorCubesCount -= 3;
        if (colorCubesCount == 0)
        {
            Timer.Instance.StopTimer();
            Invoke("EndGameProcess", 2f);
        }
    }
    void MatchWrong()
    {
        Debug.Log("Match Wrong");
        Level3Calculator.Instance.wrongSelectCount++;
        if (Level3Calculator.Instance.Score > 30f)
        {
            Level3Calculator.Instance.Score -= 30f;
        }
        else if (Level3Calculator.Instance.Score <= 30f)
        {
            Level3Calculator.Instance.Score = 0;
        }

        Invoke("HideSelectedColors", 1f);
        Invoke("SetCanSelect", 1.1f);
    }

    IEnumerator FlipSelectedCubes()
    {
        canSelect = false;
        int count = 0;
        while (count < 18)
        {
            _selectedCubes[0].gameObject.transform.Rotate(0, 0, -10);
            _selectedCubes[1].gameObject.transform.Rotate(0, 0, -10);
            _selectedCubes[2].gameObject.transform.Rotate(0, 0, -10);
            count++;
            yield return new WaitForSeconds(0.05f);
        }

        foreach(GameObject obj in _selectedCubes)
        {
            obj.gameObject.transform.position += new Vector3(0, -0.3f, 0);
            obj.gameObject.transform.localScale += new Vector3(0.25f, 0, 0.25f);
            obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        SetCanSelect();
    }


    public void TimesUpProcess()
    {
        timesUpScreen.SetActive(true);
        canSelect = false;
        Time.timeScale = 0f;
    }
    public void PauseGameProcess()
    {
        canSelect = false;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }
    void EndGameProcess()
    {
        Debug.Log("Game Ended...!!!");
        gameEnded = true;
        Time.timeScale = 0f;
        canSelect = false;
        endGameScreen.SetActive(true);
        Level3Calculator.Instance.SetEndGameText();
    }


    public void SetCanSelect()
    {
        canSelect = true;
    }
    void SetFalse()
    {
        for (int j = 0; j < isCubeColored.Length; j++)
        {
            isCubeColored[j] = false;
        }
    }
}
