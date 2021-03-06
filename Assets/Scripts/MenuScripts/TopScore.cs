﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TopScore {

	public struct PlayerScore{
        public string name;
        public int score;
		public PlayerScore(string name, int score){
			this.name = name;
			this.score = score;
		}
	}

	private List<PlayerScore> scoreList = new List<PlayerScore>();
	private string scorePath;

	public TopScore(){
		scorePath = Application.persistentDataPath + "/topscore.txt";
		LoadScore();
	}

	void LoadScore(){
        if (!System.IO.File.Exists(scorePath))
        {
            System.IO.File.WriteAllText(scorePath,"Player1,0\nPlayer1, 0\nPlayer1, 0\nPlayer1, 0\nPlayer1, 0\nPlayer1, 0\nPlayer1, 0\nPlayer1, 0\nPlayer1, 0\nPlayer1, 0\n");
        }
        StreamReader reader = new StreamReader(scorePath);
		scoreList.Clear();
		if (reader != null){
			string line;
			while((line = reader.ReadLine()) != null){
				string[] parts = line.Split(',');
				if (parts.Length == 2)
				{
					PlayerScore playerScore = new PlayerScore(parts[0], int.Parse(parts[1]));
					scoreList.Add(playerScore);
				}
				else
					Debug.Log("File has wrong format for score! ");
			}
		}
		reader.Close();
	}

	void SaveScore(){
		Stream stream = new FileStream(scorePath, FileMode.Truncate);
		StreamWriter writer = new StreamWriter(stream);
		writer.Write(ScoreToTxt());
		writer.Close();
	}

    /// <summary>
    /// Adds the score to right position and save it into txt file.
    /// </summary>
    /// <param name="player">Player's name.</param>
    /// <param name="score">Score.</param>
	public void AddScore(string player, int score){
		AddScore(new PlayerScore(player, score));
        SaveScore();
    }

    /// <summary>
    /// Gets the rank of the score.
    /// </summary>
    /// <returns>The rank.</returns>
    /// <param name="score">Score.</param>
	public int GetRank(int score){
        for (int i = 0; i < scoreList.Count; i++)
            if (scoreList[i].score < score)
                return i;

        return scoreList.Count;
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TopScore"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TopScore"/>.</returns>
	public override string ToString(){
		string txt = "";
        for (int i = 0; i < scoreList.Count; i++)
        {
            txt += AddSpace(scoreList[i].name) + AddSpace(scoreList[i].score + "") + "\n";
        }
        return txt;
	}

	string ScoreToTxt(){
		return ScoreToTxt(10);
	}

	string ScoreToTxt(int maxPosition)
    {
        string txt = "";
		for (int i = 0; i < maxPosition && i < scoreList.Count; i++)
        {
			//txt += AddSpace(scoreList[i].name) + AddSpace(scoreList[i].score + "") + "\n";
			txt += scoreList[i].name + "," + scoreList[i].score + "\n";
        }
        return txt;
    }

	string AddSpace(string s){
		return AddSpace(s, 10-s.Length);
	}

	string AddSpace(string s, int n){
		string newString = s;
		for (int i = 0; i < n; i++){
			newString += " ";
		}
		return newString;
	}

	void AddScore(PlayerScore playerScore)
    {
        scoreList.Insert(GetRank(playerScore.score), playerScore);
    }
}
