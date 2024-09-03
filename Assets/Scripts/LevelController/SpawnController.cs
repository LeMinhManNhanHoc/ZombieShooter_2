using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private GameObject enemyGO;
    private int spawnPointIndex;

    private float offsetX;
    private float offsetZ;

    public void EnableSpawning(bool enable)
    {
        if(enable)
        {
            InvokeRepeating("SpawnEnemy", 1f, LevelData.Instance.SpawnRate);
        }
        else
        {
            CancelInvoke("SpawnEnemy");
        }
    }

    private void SpawnEnemy()
    {
        enemyGO = LevelData.Instance.EnemyPool.GetPooledObject();
        enemyGO.transform.position = transform.position + RandomOffset();
        enemyGO.SetActive(true);
    }

    private Vector3 RandomOffset()
    {
        offsetX = Random.Range(0f, 10f);
        offsetZ = Random.Range(0f, 10f);

        return new Vector3(offsetX, 0f, offsetZ);
    }
}
