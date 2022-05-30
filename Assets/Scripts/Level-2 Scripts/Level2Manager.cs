using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;

    [SerializeField]
    GameObject cubePrefab, timerObj;

    GameObject[] _colorCubes = new GameObject[30];
    GameObject[] _flippedCubes = new GameObject[30];
    GameObject[] _selectedCubes = new GameObject[2];

    public Color[] _colorsOfCubes = new Color[30];
    Color[] colors = new Color[15];

    List<int> indexList = new List<int>();
    int[] _selectedIndex = new int[2];

    bool[] isCubeColored = new bool[30];
    public bool isColorHiding, canSelect;

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
        colors[7] = new Color(0.157f, 0.157f, 0.157f);
        colors[8] = new Color(1f, 0.275f, 0f);
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
        
    }

    IEnumerator CreateCubes()
    {
        canSelect = false;
        for (int i = 0; i < 30; i++)
        {
            _colorCubes[i] = Instantiate(cubePrefab, instantiateAnchor, Quaternion.identity) as GameObject;
            _colorCubes[i].name = "ColorCube" + i;
            //_colorCubes[i].GetComponent<Level1MouseFeedback>()._index = i;
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
        //HideColors();

        //timerObj.SetActive(true);
        Timer.Instance.SetDuration(1f, 0f);
        Timer.Instance.StartTimer();
    }



    void SetFalse()
    {
        for (int j = 0; j < isCubeColored.Length; j++)
        {
            isCubeColored[j] = false;
        }
    }
}