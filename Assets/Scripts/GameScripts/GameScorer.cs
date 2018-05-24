using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScorer : MonoBehaviour {

    public MasterReferences master;
    int allyCount = 0, enemyDeathCount;

    public TextMesh allyText, scoreText;
    int initScore = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("GameScore")) {
            initScore = PlayerPrefs.GetInt("GameScore");
        }

        RefreshAllyText();
        RefreshScoreText();
    }

    public void Death ()
    {
        allyText.text = "xxx";
        scoreText.text = "xxxxx";
    }

    public void AddAlly ()
    {
        allyCount++;
        RefreshAllyText();
        RefreshScoreText();
    }

    public void KillAlly()
    {
        allyCount--;
        RefreshAllyText();
        RefreshScoreText();
    }

    void RefreshAllyText ()
    {
        allyText.text = "" + allyCount;
    }

    void RefreshScoreText()
    {
        scoreText.text = "" + (initScore + master.scorer.GetAllyCount() + master.scorer.GetEnemyDeaths());
    }

    public void AddEnemyDeath ()
    {
        enemyDeathCount++;
        RefreshScoreText();
    }

    public int GetAllyCount ()
    {
        return allyCount;
    }

    public int GetEnemyDeaths()
    {
        return enemyDeathCount;
    }
}
