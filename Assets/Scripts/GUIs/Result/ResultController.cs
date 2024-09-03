using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text content;
    [SerializeField] Text totalScore;

    public int killScore;
    public int hpScore;

    private int totalKillScore = 0;
    private int totalHpScore = 0;

    public void Init(bool isWin)
    {
        this.gameObject.SetActive(true);

        totalKillScore = ScoreManager.Instance.GetKilledZombie() * killScore;
        totalHpScore = Mathf.RoundToInt(CombatManager.Instance.Player.PlayerRemainHP) * hpScore;

        title.text = isWin ? "MISSION COMPLETED" : "MISSION FAILED";
        content.text = $"KILLED: {ScoreManager.Instance.GetKilledZombie()} x {killScore} = {totalKillScore}\n\n";
        content.text += $"REMAIN HP: {CombatManager.Instance.Player.PlayerRemainHP} x {hpScore} = {totalHpScore}";

        totalScore.text = $"TOTAL SCORE: {totalKillScore + totalHpScore}";

    }

}
