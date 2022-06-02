using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2MouseFeedback : MonoBehaviour
{
    public static Level2MouseFeedback Instance;
    Renderer _renderer;
    public bool isFlipped;
    public int _index;

    private void Awake()
    {
        if(Instance == null)
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
        Debug.Log(Level2Manager.Instance.canSelect);
        if (Level2Manager.Instance.canSelect && Level2Manager.Instance.isColorHiding)
        {
            if (!isFlipped)
            {
                _renderer.material.color = Level2Manager.Instance._colorsOfCubes[_index];
                Level2Manager.Instance.CubeSelect(_index);
            }
        }
    }
}
