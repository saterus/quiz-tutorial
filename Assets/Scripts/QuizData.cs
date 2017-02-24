using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class QuizData
{
	public RoundData[] allRoundData;


	private static string fileName = "quizData.json";

	public static QuizData LoadFromFile()
	{
		QuizData quizData;

		string path = fullPath();

		try
		{
			quizData = JsonUtility.FromJson<QuizData>(File.ReadAllText(path));
		}
		catch (IOException ex)
		{
			Debug.LogErrorFormat("Cannot load QuizData file: {0} - {1}!", path, ex.ToString());
			quizData = new QuizData();
		}

		return quizData;
	}

	public void SaveToFile()
	{
		string data = JsonUtility.ToJson(this);
		string path = QuizData.fullPath();

		File.WriteAllText(path, data);
	}

	private static string fullPath()
	{
		return Path.Combine(Application.streamingAssetsPath, fileName);
	}
}
