using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [SerializeField] Text scoreText;

    private int scoreCounter;
    public int ScoreCounter {  get { return scoreCounter; } }

    private void Start()
    {
        scoreCounter = 0;
        if (scoreText != null) scoreText.text = scoreCounter.ToString();
    }

    public void AddScore(int score)
    {
        scoreCounter += score;

        if (scoreText != null) scoreText.text = scoreCounter.ToString();
    }
}
