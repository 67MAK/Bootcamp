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
        // Awake metodu içinde DestPoints objesinin çocuk objeleri points[] dizisinin içine alýnýyor
        // points[] ile NPC'lerin gideceði noktalar hesaplanmak üzere tutuluyor.
        _destPoints = GameObject.Find("DestPoints");// Hierarchy içinde ismi "DestPoints" olan nesneyi bulan kod
        points = new Transform[_destPoints.transform.childCount];
        for(int i = 0; i < points.Length; i++) // Her bir çocuk obje sýrasýyla diziye atanýyor.
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
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime); // NPC'nin sýradaki varýþ noktasýna gitmesini saðlayan kod
        if(Vector3.Distance(transform.position, movePoint.position) <= 0) // NPC'nin varýþ noktasýna ulaþma durumunu kontrol eden if bloðu
        {
            if(pointIndex >= points.Length-1) // Son varýþ noktasýna geldiðinde NPC Destroy ediliyor.
            {
                Destroy(gameObject);
            }
            else // Aksi durumda bir sonraki nokta varýþ noktasý olarak ayarlanýyor.
            {
                pointIndex++;
                movePoint = points[pointIndex];
            }
        }
        transform.LookAt(movePoint.position); // NPC varýþ noktasýna ilerlerken yüzünü noktaya dönmesini saðlayan kod(Gerçekçi görünüm için gerekli)
    }
}
