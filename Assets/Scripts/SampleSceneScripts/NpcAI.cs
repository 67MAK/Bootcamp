using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    public float speed;
    int pointIndex;
    Transform movePoint;
    public GameObject _destPoints;
    public static Transform[] points;

    void Awake()
    { 
        // Awake metodu i�inde DestPoints objesinin �ocuk objeleri points[] dizisinin i�ine al�n�yor
        // points[] ile NPC'lerin gidece�i noktalar hesaplanmak �zere tutuluyor.
        _destPoints = GameObject.Find("DestPoints");// Hierarchy i�inde ismi "DestPoints" olan nesneyi bulan kod
        points = new Transform[_destPoints.transform.childCount];
        for(int i = 0; i < points.Length; i++) // Her bir �ocuk obje s�ras�yla diziye atan�yor.
        {
            points[i] = _destPoints.transform.GetChild(i);
        }
    }
    void Start()
    {
        pointIndex = 0;
        movePoint = points[pointIndex];
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime); // NPC'nin s�radaki var�� noktas�na gitmesini sa�layan kod
        if(Vector3.Distance(transform.position, movePoint.position) <= 0) // NPC'nin var�� noktas�na ula�ma durumunu kontrol eden if blo�u
        {
            if(pointIndex >= points.Length-1) // Son var�� noktas�na geldi�inde NPC Destroy ediliyor.
            {
                Destroy(gameObject);
            }
            else // Aksi durumda bir sonraki nokta var�� noktas� olarak ayarlan�yor.
            {
                pointIndex++;
                movePoint = points[pointIndex];
            }
        }
        transform.LookAt(movePoint.position); // NPC var�� noktas�na ilerlerken y�z�n� noktaya d�nmesini sa�layan kod(Ger�ek�i g�r�n�m i�in gerekli)
    }
}
