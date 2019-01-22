using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Song
{
	[Header("Audio")]
	public AudioClip audio;
	[Header("Correct Answer")]
	public string CorrectAnswer;
	[Header("False Answers")]
	public string[] FalseAnswers = new string[3];
}

[CreateAssetMenu(fileName="Create Category",menuName="Category")]
public class Category : ScriptableObject {


	public string CategoryName = "default";
	public List<Song> Songs;
}
