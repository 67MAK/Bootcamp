using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1MouseFeedback : MonoBehaviour
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
        Debug.Log(Level1Manager.Instance.canSelect);
        if (Level1Manager.Instance.canSelect && Level1Manager.Instance.isColorHiding)
        {
            _renderer.material.color = Level1Manager.Instance._colorsOfCubes[_index];
            Level1Manager.Instance.CubeSelect(_index);
        }
    }
}
