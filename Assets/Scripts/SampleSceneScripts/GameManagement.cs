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
        for(int i = 0; i < numOfWave; i++) // Parametre olarak al�nan, olu�turulacak dalga say�s�(numberOfWaves -> numOfWave) kadar d�nen for d�ng�s�
        {
            int enemyCount = ((i + 1) * (i + 2)) + 1; // Her dalga i�in d��man say�s� belirleniyor.
            if(enemyCount < 26) // Belirlenen d��man say�s� 26'dan k���kse atan�yor.
                numberOfEnemies = enemyCount;
            else // Aksi halde 25 say�s� atan�yor. (Bu say� test ama�l�d�r. De�i�tirilebilir)
                numberOfEnemies = 25;

            float waveTime = ((float)enemyCount * 1.5f) * 2.5f;

            if (waveTime < 75) // D��man say�s� belirlenirkenki duruma benzer. Dalgalar aras� beklenecek s�re belirleniyor. (D��man say�s�na ba�l� olarak)
                timeBetweenWaves = Mathf.Round(waveTime + 0.4f); // Her zaman �stteki say�ya yuvarlanm�� halde e�itlenmesi i�in say�ya 0.4 eklenerek Round() fonk. uygulan�yor.
            else
                timeBetweenWaves = 75f;

            StartCoroutine(spawnEnemy(numberOfEnemies));
            yield return new WaitForSeconds(timeBetweenWaves);

        }
    }
    IEnumerator spawnEnemy(int numOfEnemy)
    {
        for( int i = 0; i < numOfEnemy; i++) // Yine parametre olarak al�nan dalga i�inde �retilecek d��man say�s�(numberOfEnemies -> numOfEnemy) kadar d�nen for d�g�s�
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(_enemyObject, _spawnPoint.position, Quaternion.identity);
        }
    }
}
