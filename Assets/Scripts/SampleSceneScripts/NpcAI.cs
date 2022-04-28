using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    public float speed;
    public int MaxNum;
    int pointIndex;
    Transform movePoint;
    public Transform[] points;
    void Start()
    {
        pointIndex = 0;
        movePoint = points[pointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position) <= 0)
        {
            if(pointIndex > MaxNum)
            {
                Destroy(gameObject);
            }

            pointIndex++;
            movePoint = points[pointIndex];
        }
        transform.LookAt(movePoint.position);
        /*Vector3 pos = movePoint.position - transform.position;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);*/
    }
}
