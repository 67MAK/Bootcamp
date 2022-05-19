using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] GameObject _1enemyObject, _2enemyObject, _3enemyObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startWave()
    {
        yield return null;
    }
}
