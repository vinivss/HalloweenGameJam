using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    private int waveIndex = 0;
    public int level = 0;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public int SceneNumber;

    public Transform[] spawnlist;
    //private Vector3 spawnPos;
    private Transform spawnPoint;

    
    public TMP_Text eCount;
    public TMP_Text wCount;

    void Update()
    {
        EnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;

        eCount.text = "Enemies Remaining: " + EnemiesAlive;
        wCount.text = "Wave: " + level + "/" + waves.Length;

       // Debug.Log("EnemiesAlive: " + EnemiesAlive);
        if (level >= waves.Length)
        {
            WinLevel();
        }
        if (EnemiesAlive > 0)
        {
            return;
        }
        Debug.Log("Countdown: " + countdown);

        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

    }
    public void WinLevel()
    {
        level = 0;
        waveIndex = 0;
        SceneManager.LoadScene(SceneNumber);
    }
    IEnumerator SpawnWave()
    {
        level++;
        Wave wave = waves[waveIndex];
        Debug.Log("Wave Incoming");
        
       for (int i = 0; i < wave.count1; i++)
       {
           SpawnEnemy(wave.enemy1);
           yield return new WaitForSeconds(1f / wave.rate1);
       }
       for (int i = 0; i < wave.count2; i++)
       {
           SpawnEnemy(wave.enemy2);
           yield return new WaitForSeconds(1f / wave.rate2);
       }
       for (int i = 0; i < wave.count3; i++)
       {
           SpawnEnemy(wave.enemy3);
           yield return new WaitForSeconds(1f / wave.rate3);
       }
        for (int i = 0; i < wave.count4; i++)
        {
            SpawnEnemy(wave.enemy4);
            yield return new WaitForSeconds(1f / wave.rate4);
        }
        waveIndex++;
   }

    void SpawnEnemy(GameObject enemy)
    {
        int spawnIndex = Random.Range(0, spawnlist.Length);
        spawnPoint = spawnlist[spawnIndex];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (var w in spawnlist)
        {
            Gizmos.DrawSphere(w.transform.position, 1.0f);
        };
    }

}
