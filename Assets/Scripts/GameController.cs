using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public SimpleObjectPool answerButtonObjectPool;
	public GameObject answersPanel;
	public Text questionDisplayText;
	public Text scoreDisplayText;
	public Text timeRemainingDisplayText;
	public GameObject questionPanel;
	public GameObject roundOverPanel;

	private DataController dataController;
	private RoundData currentRoundData;
	private QuestionData[] questionPool;
	private bool isRoundActive;
	private float timeRemaining;
	private int questionIndex;
	private int playerScore;

	void Start()
	{
		dataController = FindObjectOfType<DataController>();
		currentRoundData = dataController.GetCurrentRoundData();
		questionPool = currentRoundData.questions;

		timeRemaining = currentRoundData.timeLimitInSeconds;
		UpdateTimeRemainingDisplay();

		playerScore = 0;
		questionIndex = 0;

		ShowQuestion();
		isRoundActive = true;
	}

	private void ShowQuestion()
	{
		RemoveAnswerButtons();
		QuestionData questionData = questionPool[questionIndex];
		questionDisplayText.text = questionData.questionText;

		foreach (var answer in questionData.answers)
		{
			GameObject answerButtonRoot = answerButtonObjectPool.GetObject();
			answerButtonRoot.transform.SetParent(answersPanel.transform, false);

			AnswerButton answerButton = answerButtonRoot.GetComponent<AnswerButton>();
			answerButton.Setup(answer);
		}
	}

	private void RemoveAnswerButtons()
	{
		foreach (AnswerButton button in answersPanel.GetComponentsInChildren<AnswerButton>())
		{
			answerButtonObjectPool.ReturnObject(button.gameObject);
		}
	}

	public void AnswerButtonClicked(bool isCorrect)
	{
		if (isCorrect)
		{
			playerScore += currentRoundData.pointsAddedForCorrectAnswer;
			scoreDisplayText.text = "Score: " + playerScore;
		}

		if (questionPool.Length > questionIndex + 1)
		{
			questionIndex++;
			ShowQuestion();
		}
		else
		{
			EndRound();
		}
	}

	public void EndRound()
	{
		isRoundActive = false;
		roundOverPanel.SetActive(true);
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("MenuScreen");
	}

	private void UpdateTimeRemainingDisplay()
	{
		timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
	}

	void Update()
	{
		if (isRoundActive)
		{
			timeRemaining -= Time.deltaTime;
			UpdateTimeRemainingDisplay();

			if (timeRemaining <= 0)
			{
				EndRound();
			}
		}
	}
}
