using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScorer : MonoBehaviour {

    public MasterReferences master;
    int allyCount = 0, enemyDeathCount;

    public TextMesh allyText, scoreText;
    int initScore = 0;
    bool dead = false;

    public bool allowBonus;
    bool talliedBonus = false;
    int bonus;

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
        dead = true;
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
        if (dead) return;
        allyText.text = "" + allyCount;
    }

    void RefreshScoreText()
    {
        if (dead) return;
        scoreText.text = "" + (initScore + master.scorer.GetAllyCount() + (master.scorer.GetEnemyDeaths() * 5) + bonus );
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

    public int GetBonusRoundPts ()
    {
        if (!talliedBonus && allowBonus)
        {
            bonus = (initScore + master.scorer.GetAllyCount() + (master.scorer.GetEnemyDeaths() * 5)) / 4; //get 25% of your current score as a bonus for finishing the level
            talliedBonus = true;
        }
        RefreshScoreText();
        return bonus;
    }
}
