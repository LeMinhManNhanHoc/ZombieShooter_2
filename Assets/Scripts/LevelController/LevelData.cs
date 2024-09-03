using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoSingleton<LevelData>
{
    [SerializeField] ObjectPool enemyPool;
    [SerializeField] float spawnRate;

    public ObjectPool EnemyPool { get { return enemyPool; } }
    public float SpawnRate { get { return spawnRate; } }

    private void Start()
    {
        enemyPool.InitPool();
    }
}
