using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [SerializeField] Text scoreText;

    private int killedZombies;

    private void Start()
    {
        killedZombies = 0;
        scoreText.text = killedZombies.ToString();
    }

    public void AddKilled()
    {
        killedZombies++;
        scoreText.text = killedZombies.ToString();
    }

    public int GetKilledZombie()
    {
        return killedZombies;
    }
}
