using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;

    [SerializeField]
    GameObject cubePrefab, timerObj, timesUpScreen, endGameScreen;
    [SerializeField] public GameObject pauseScreen;

    GameObject[] _colorCubes = new GameObject[30];
    GameObject[] _flippedCubes = new GameObject[30];
    GameObject[] _selectedCubes = new GameObject[2];


    public Color[] _colorsOfCubes = new Color[30];
    Color[] colors = new Color[15];

    List<int> indexList = new List<int>();
    int[] _selectedIndex = new int[2];

    bool[] isCubeColored = new bool[30];
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
        colors[14] = new Color(0.353f, 0.353f, 0.353f);
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
        for (int i = 0; i < 30; i++)
        {
            _colorCubes[i] = Instantiate(cubePrefab, instantiateAnchor, Quaternion.identity) as GameObject;
            _colorCubes[i].name = "ColorCube" + i;
            _colorCubes[i].GetComponent<Level2MouseFeedback>()._index = i;
            indexList.Add(i);
            colorCubesCount++;
            instantiateAnchor = instantiateAnchor + new Vector3(1.25f, 0, 0);
            if ((i + 1) % 5 == 0)
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
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(2f);
        HideColors();

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
        _selectedCubes[0].GetComponent<MeshRenderer>().material.color = Color.white;
        _selectedCubes[1].GetComponent<MeshRenderer>().material.color = Color.white;
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
        Debug.Log("selectedCount before if : " + selectedCount);
        if(selectedCount == 0)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount++;
        }
        else if(selectedCount == 1 && _selectedCubes[0].GetComponent<Level2MouseFeedback>()._index != selectedIndex)
        {
            _selectedCubes[selectedCount] = _colorCubes[selectedIndex];
            _selectedIndex[selectedCount] = selectedIndex;
            selectedCount = 0;
            canSelect = false;
            CheckColor();
        }
        Debug.Log("selectedCount after if : " + selectedCount);
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
        Level2Calculator.Instance.Score += 50f;
        StartCoroutine(FlipSelectedCubes());
        _flippedCubes[_selectedIndex[0]] = _colorCubes[_selectedIndex[0]];
        _flippedCubes[_selectedIndex[1]] = _colorCubes[_selectedIndex[1]];
        _flippedCubes[_selectedIndex[0]].GetComponent<Level2MouseFeedback>().isFlipped = true;
        _flippedCubes[_selectedIndex[1]].GetComponent<Level2MouseFeedback>().isFlipped = true;
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
        Level2Calculator.Instance.wrongSelectCount++;
        if (Level2Calculator.Instance.Score > 30f)
        {
            Level2Calculator.Instance.Score -= 30f;
        }
        else if (Level2Calculator.Instance.Score <= 30f)
        {
            Level2Calculator.Instance.Score = 0;
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
        Level2Calculator.Instance.SetEndGameText();
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
