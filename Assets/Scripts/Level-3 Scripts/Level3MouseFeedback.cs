using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3MouseFeedback : MonoBehaviour
{
    public static Level3MouseFeedback Instance;
    Renderer _renderer;
    public bool isFlipped;
    public int _index;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    void Update()
    {

    }

    private void OnMouseDown()
    {
        //Debug.Log(_index + ". index color : " + Level1Manager.Instance._colorsOfCubes[_index]);
        Debug.Log(Level3Manager.Instance.canSelect);
        if (Level3Manager.Instance.canSelect && Level3Manager.Instance.isColorHiding)
        {
            if (!isFlipped)
            {
                _renderer.material.color = Level3Manager.Instance._colorsOfCubes[_index];
                //Level3Manager.Instance.CubeSelect(_index);
            }
        }
    }
}
