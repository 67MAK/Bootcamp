using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] GameObject _enemyObject;
    [SerializeField] Transform _spawnPoint;
    float timeBetweenWaves = 12f;
    [SerializeField] int numberOfWaves = 5;
    [SerializeField] int numberOfEnemies;
    void Start()
    {
        //StartCoroutine(spawnEnemy(numberOfEnemies));
        StartCoroutine(spawnWave(numberOfWaves));
    }

    
    void Update()
    {
        
    }

    IEnumerator spawnWave(int numOfWave)
    {
        for(int i = 0; i < numOfWave; i++) // Parametre olarak alýnan, oluþturulacak dalga sayýsý(numberOfWaves -> numOfWave) kadar dönen for döngüsü
        {
            int enemyCount = ((i + 1) * (i + 2)) + 1; // Her dalga için düþman sayýsý belirleniyor.
            if(enemyCount < 26) // Belirlenen düþman sayýsý 26'dan küçükse atanýyor.
                numberOfEnemies = enemyCount;
            else // Aksi halde 25 sayýsý atanýyor. (Bu sayý test amaçlýdýr. Deðiþtirilebilir)
                numberOfEnemies = 25;

            float waveTime = ((float)enemyCount * 1.5f) * 2.5f;

            if (waveTime < 75) // Düþman sayýsý belirlenirkenki duruma benzer. Dalgalar arasý beklenecek süre belirleniyor. (Düþman sayýsýna baðlý olarak)
                timeBetweenWaves = Mathf.Round(waveTime + 0.4f); // Her zaman üstteki sayýya yuvarlanmýþ halde eþitlenmesi için sayýya 0.4 eklenerek Round() fonk. uygulanýyor.
            else
                timeBetweenWaves = 75f;

            StartCoroutine(spawnEnemy(numberOfEnemies));
            yield return new WaitForSeconds(timeBetweenWaves);

        }
    }
    IEnumerator spawnEnemy(int numOfEnemy)
    {
        for( int i = 0; i < numOfEnemy; i++) // Yine parametre olarak alýnan dalga içinde üretilecek düþman sayýsý(numberOfEnemies -> numOfEnemy) kadar dönen for dögüsü
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(_enemyObject, _spawnPoint.position, Quaternion.identity);
        }
    }
}
