using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageBusSystem;

public class CombatManager : MonoSingleton<CombatManager>
{
    [SerializeField] PlayerController player;
    [SerializeField] SpawnController[] spawnPoints;
    [SerializeField] ResultController resultController;

    [SerializeField] bool canSpawnBoss;
    [SerializeField] ObjectPool bossPool;

    public PlayerController Player {  get { return player; } }

    private bool IsGameOver;
    public bool IsWin { get { return TimeManager.Instance.IsTimeOut && !player.CheckIsDead(); } }

    private int spawnPointIndex;
    private GameObject enemyGO;

    private void Start()
    {
        IsGameOver = false;
        BeginSpawn();

        if (canSpawnBoss)
        {
            bossPool.InitPool();
            MessageBus.Subsribe(MessageType.MINUTE_PASS, SpawnBoss);
        }
    }

    private void OnDestroy()
    {
        MessageBus.Unsubscribe(MessageType.MINUTE_PASS, SpawnBoss);
    }

    private void BeginSpawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].EnableSpawning(true);
        }
    }

    private void Update()
    {
        if (IsGameOver) return;

        if(TimeManager.Instance.IsTimeOut || player.CheckIsDead())
        {
            IsGameOver = true;

            Time.timeScale = 0f;

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].EnableSpawning(false);
            }

            resultController.Init(IsWin);
        }
    }

    public void ReturnMainMenu()
    {
        SceneLoadSystem.Instance.LoadScene(0);
    }

    public void SpawnBoss(object data)
    {
        float minuteLeft = (float)data;

        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

        if(minuteLeft < 2f)
        {
            GameObject bossGO = bossPool.GetPooledObject();
            bossGO.transform.position = spawnPoints[randomSpawnPoint].transform.position;
            bossGO.SetActive(true);

            SoundSystem.Instance.PlaySFX("BossAppear");
        }
    }
}
