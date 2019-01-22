using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {

	public bool Player1;
	public bool FalseAnswer;
	public Text ButtonText;


	public void SetButton(string txt, bool b)
	{
		FalseAnswer = b;
		ButtonText.text = txt;
	}
}
