using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoSingleton<CombatManager>
{
    [SerializeField] PlayerController player;
    [SerializeField] SpawnController[] spawnPoints;
    [SerializeField] ResultController resultController;

    public PlayerController Player {  get { return player; } }

    private bool IsGameOver;
    public bool IsWin { get { return TimeManager.Instance.IsTimeOut && !player.CheckIsDead(); } }

    private int spawnPointIndex;
    private GameObject enemyGO;

    private void Start()
    {
        IsGameOver = false;
        BeginSpawn();
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
}
