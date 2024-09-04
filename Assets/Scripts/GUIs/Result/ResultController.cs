using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text content;
    [SerializeField] Text totalScore;

    [SerializeField] int killScoreMult = 10;
    [SerializeField] int hpScore = 100;

    private int totalKillScore = 0;
    private int totalHpScore = 0;

    public void Init(bool isWin)
    {
        this.gameObject.SetActive(true);

        totalKillScore = ScoreManager.Instance.ScoreCounter * killScoreMult;
        totalHpScore = Mathf.RoundToInt(CombatManager.Instance.Player.PlayerRemainHP) * hpScore;

        title.text = isWin ? "MISSION COMPLETED" : "MISSION FAILED";
        content.text = $"KILLED SCORE: {ScoreManager.Instance.ScoreCounter} x {killScoreMult} = {totalKillScore}\n\n";
        content.text += $"REMAIN HP: {CombatManager.Instance.Player.PlayerRemainHP} x {hpScore} = {totalHpScore}";

        totalScore.text = $"TOTAL SCORE: {totalKillScore + totalHpScore}";

        Destroy(ScoreManager.Instance.gameObject);
    }

}
