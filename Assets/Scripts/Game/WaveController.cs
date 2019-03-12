using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    [Header("Start and End")]
    public Transform start;
    public Transform end;

    [Header("Enemies")]
    public GameObject enemy; // Add Enemy prefab
    public GameObject boss; // Add Boss prefab

    [Header("Controllers")]
    public EnemyController ec;

    [Header("Wave Settings")]
    public float waveDuration = 15f;
    private float countDown = 0f;
    private int waveNumber;

    [Header("Game UI")]
    public Text waveCounter; 

    void Start()
    {
        waveNumber = 1;
    }

    void Update()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SendWave());
            countDown = waveDuration;
        }
        countDown -= Time.deltaTime;
    }

    IEnumerator SendWave()
    {
        waveCounter.text = "Wave " + waveNumber.ToString();
        waveCounter.CrossFadeAlpha(1, 2f, false);
        for (int i = 0; i < waveNumber; i++)
        {   
            if(waveNumber % 3 == 0 && i == 2)
            {
                SpawnEnemy(boss);
            }
            else
            {
                SpawnEnemy(enemy);
            }
            yield return new WaitForSeconds(1f);
        }
        waveNumber++;
        waveCounter.CrossFadeAlpha(0, 2f, false);
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject enemyGO = (GameObject)Instantiate(enemy, start.position, start.rotation);
        EnemyController ec = enemyGO.GetComponent<EnemyController>();

        if (ec != null)
        {
            ec.SetDestination(end);
        }
    }

    public int getWaveNumber()
    {
        return this.waveNumber;
    }
}
