using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;

    [SerializeField]
    GameObject cubePrefab, timerObj, timesUpScreen, endGameScreen, guideText;
    [SerializeField] public GameObject pauseScreen;

    GameObject[] _colorCubes = new GameObject[16];
    GameObject[] _flippedCubes = new GameObject[16];
    GameObject[] _selectedCubes = new GameObject[2];


    public Color[] _colorsOfCubes = new Color[16];
    Color[] colors = new Color[8];

    List<int> indexList = new List<int>();
    int[] _selectedIndex = new int[2];

    bool[] isCubeColored = new bool[16];
    bool[] isCubeFlipped = new bool[16];
    public bool isColorHiding, canSelect, gameEnded;

    Vector3 instantiateAnchor = Vector3.zero;

    int colorCubesCount = 0, selectedCount = 0;
    int rand;

    private void Awake()
    {
        if (Instance == null)
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
        colors[7] = new Color(0.157f, 0.157f, 0.157f);
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
        for (int i = 0; i < 16; i++)
        {
            _colorCubes[i] = Instantiate(cubePrefab, instantiateAnchor, Quaternion.identity) as GameObject;
            isCubeFlipped[i] = false;
            _colorCubes[i].name = "ColorCube" + i;
            _colorCubes[i].GetComponent<Level1MouseFeedback>()._index = i;
            indexList.Add(i);
            colorCubesCount++;
            instantiateAnchor = instantiateAnchor + new Vector3(1.25f, 0, 0);
            if ((i + 1) % 4 == 0)
            {
                instantiateAnchor = new Vector3(0, 0, instantiateAnchor.z);
                instantiateAnchor = instantiateAnchor + new Vector3(0, 0, 1.25f);
            }
            yield return new WaitForSeconds(0.2f);
        }
        instantiateAnchor = Vector3.zero;
        StartCoroutine(SetColors());
    }
    IEnumerator SetColors()
    {
        int setIndex, k = 0, colorIndex = 0;
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
                if (k == 2)
                {
                    k = 0;
                    colorIndex++;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2f);
        HideColors();
        guideText.SetActive(false);
        timerObj.SetActive(true);
        Timer.Instance.SetDuration(1f, 0f);
        Timer.Instance.StartTimer();
    }
    void HideColors()
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
        //Debug.Log("phaseCount : " + phaseCount + "------ phases : " + phases);

    }
    void HideSelectedColors()
    {
        _selectedCubes[0].GetComponent<MeshRenderer>().material.color = Color.white;
        _selectedCubes[1].GetComponent<MeshRenderer>().material.color = Color.white;
    }
    void ShowColors()
    {
        for (int i = 0; i < _colorCubes.Length; i++)
        {
            if (isCubeColored[i] && _colorCubes[i] != null)
            {
                _colorCubes[i].GetComponent<MeshRenderer>().material.color = _colorsOfCubes[i];
            }
        }
        isColorHiding = false;
        canSelect = false;
    }

    public void CubeSelect(int selectedIndex)
    {
        if (selectedCount == 0)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount++;
        }
        else if (selectedCount == 1 && _selectedCubes[0].GetComponent<Level1MouseFeedback>()._index != selectedIndex)
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
        if (_selectedCubes[0].GetComponent<MeshRenderer>().material.color == _selectedCubes[1].GetComponent<MeshRenderer>().material.color)
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
        Level1Calculator.Instance.Score += 50f;
        StartCoroutine(FlipSelectedCubes());
        _flippedCubes[_selectedIndex[0]] = _colorCubes[_selectedIndex[0]];
        _flippedCubes[_selectedIndex[1]] = _colorCubes[_selectedIndex[1]];
        _flippedCubes[_selectedIndex[0]].GetComponent<Level1MouseFeedback>().isFlipped = true;
        _flippedCubes[_selectedIndex[1]].GetComponent<Level1MouseFeedback>().isFlipped = true;
        _colorCubes[_selectedIndex[0]] = null;
        _colorCubes[_selectedIndex[1]] = null;
        colorCubesCount -= 2;
        if (colorCubesCount == 0)
        {
            Timer.Instance.StopTimer();
            Invoke("EndGameProcess", 2f);
        }
    }
    void MatchWrong()
    {
        Debug.Log("Match Wrong");
        Level1Calculator.Instance.wrongSelectCount++;
        if (Level1Calculator.Instance.Score > 30f)
        {
            Level1Calculator.Instance.Score -= 30f;
        }
        else if (Level1Calculator.Instance.Score <= 30f)
        {
            Level1Calculator.Instance.Score = 0;
        }
        
        Invoke("HideSelectedColors", 1f);
        Invoke("SetCanSelect", 1.1f);
    }
    IEnumerator FlipSelectedCubes()
    {
        canSelect = false;
        int count = 0;
        while(count < 18)
        {
            _selectedCubes[0].gameObject.transform.Rotate(0, 0, -10);
            _selectedCubes[1].gameObject.transform.Rotate(0, 0, -10);
            count++;
            yield return new WaitForSeconds(0.05f);
        }
        _selectedCubes[0].gameObject.transform.position += new Vector3(0, -0.3f, 0);
        _selectedCubes[1].gameObject.transform.position += new Vector3(0, -0.3f, 0);
        _selectedCubes[0].gameObject.transform.localScale += new Vector3(0.25f, 0, 0.25f);
        _selectedCubes[1].gameObject.transform.localScale += new Vector3(0.25f, 0, 0.25f);
        _selectedCubes[0].GetComponent<MeshRenderer>().material.color = Color.white;
        _selectedCubes[1].GetComponent<MeshRenderer>().material.color = Color.white;
        SetCanSelect();
    }

    public void TimesUpProcess()
    {
        timesUpScreen.SetActive(true);
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
        //Time.timeScale = 0f;
        canSelect = false;
        endGameScreen.SetActive(true);
        Level1Calculator.Instance.SetEndGameText();
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
