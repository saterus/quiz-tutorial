using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
	public RoundData[] allRoundData;

	private PlayerProgress playerProgress;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		allRoundData = QuizData.LoadFromFile().allRoundData;
		LoadPlayerProgress();

		SceneManager.LoadScene("MenuScreen");
	}

	public RoundData GetCurrentRoundData()
	{
		return allRoundData[0];
	}

	public void SubmitNewPlayerScore(int newScore)
	{
		if (newScore > playerProgress.highestScore)
		{
			playerProgress.highestScore = newScore;
			SavePlayerProgress();
		}
	}

	public int GetHighestPlayerScore()
	{
		return playerProgress.highestScore;
	}

	private void LoadPlayerProgress()
	{
		playerProgress = new PlayerProgress();
		playerProgress.highestScore = PlayerPrefs.GetInt("highestScore", 0);
	}

	private void SavePlayerProgress()
	{
		PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
	}
}
