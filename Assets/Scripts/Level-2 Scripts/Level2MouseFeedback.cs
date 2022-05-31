using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2MouseFeedback : MonoBehaviour
{
    Renderer _renderer;

    public int _index;

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
            _renderer.material.color = Level2Manager.Instance._colorsOfCubes[_index];
            Level2Manager.Instance.CubeSelect(_index);
        }
    }
}
