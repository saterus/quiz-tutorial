using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class QuizEditor : EditorWindow
{
	public QuizData quizData;

	[MenuItem("Window/Quiz Editor")]
	static void Init()
	{
		QuizEditor window = EditorWindow.GetWindow<QuizEditor>();
		window.Show();
	}

	void OnGUI()
	{
		if (quizData != null)
		{

			SerializedObject obj = new SerializedObject(this);
			SerializedProperty prop = obj.FindProperty("quizData");

			EditorGUILayout.PropertyField(prop, true);

			obj.ApplyModifiedProperties();

			if (GUILayout.Button("Save data"))
			{
				quizData.SaveToFile();
			}
		}

		if (GUILayout.Button("Load data"))
		{
			quizData = QuizData.LoadFromFile();
		}
	}
}
