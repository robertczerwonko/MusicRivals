using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Play_Mode : MonoBehaviour {


	[Header("Versun Panel")]
	public GameObject VersusPanel;
	public Button BEGIN_BUTTON;
	public Text P1_name;
	public Text P2_name;
	[Space(10)]
	[Header("PlayMode Holder")]
	public GameObject PlayModeHolder;
    [Header("Player 1")]
    public Text battleP1Name;
    public Text Battle1Score;
	public Text Timer1;
	public Button[] Answer_Buttons1;
    [Header("Player 2")]
    public Text battleP2Name;
    public Text Battle2Score;
	public Text Timer2;
	public Button[] Answer_Buttons2;

	[Header("Main Mode")]
	public Main_Game _MainGame;

    private int fightPointsForPlayer1;
    private int fightPointsForPlayer2;

    private AudioSource AudioSrc;
	private bool CanAnswer;
	private int songNumber;

	private Category _category;
	private List<Song> Songs;
	private int countBadAnswers;
	private Color originButtonColor;

	void Start()
	{
		originButtonColor = BEGIN_BUTTON.GetComponent<Image> ().color;
		AudioSrc = GetComponent<AudioSource> ();
		BEGIN_BUTTON.onClick.AddListener (() => BeginButton_Click ());

        SetAnswerButtons();
    }
		
	public void PresentPlayers(string p1, string p2, Category c)
	{
        VersusPanel.SetActive (true);
		Songs = c.Songs;
		SetAnswers ();
		P1_name.text = p1;
        battleP1Name.text = p1;
		P2_name.text = p2;
        battleP2Name.text = p2;
        Battle1Score.text = 0.ToString();
        Battle2Score.text = 0.ToString();
    }



    private int fightIndex = 0;
    private void BeginFight()
    {
        fightIndex++;
        if(fightIndex > 3)
        {
            PlayModeHolder.SetActive(false);           
            bool draw = (fightPointsForPlayer1 == fightPointsForPlayer2) ? true : false;
            bool p1Wins = (fightPointsForPlayer1 > fightPointsForPlayer2) ? true : false;
            _MainGame.FinishedRound(draw, p1Wins,fightPointsForPlayer1,fightPointsForPlayer2);
            fightIndex = 0;
            fightPointsForPlayer1 = 0;
            fightPointsForPlayer2 = 0;
        }
        else
        {
            SetAnswers();
            StartCoroutine(CountTime());
        }

    }
    private void SetAnswerButtons()
	{
		foreach (Button btn in Answer_Buttons1) {
			btn.GetComponent<AnswerButton> ().Player1 = true;
			btn.onClick.AddListener (() => AnswerButton_Click (btn.GetComponent<AnswerButton>().FalseAnswer,btn));
		}

		foreach (Button btn in Answer_Buttons2) {
			btn.GetComponent<AnswerButton> ().Player1 = false;
			btn.onClick.AddListener (() => AnswerButton_Click (btn.GetComponent<AnswerButton>().FalseAnswer,btn));
		}
	}
	private void AnswerButton_Click(bool FalseAnswer,Button _btn)
	{
		if (CanAnswer) {
			
			if (!FalseAnswer)
            {
				_btn.GetComponent<Image> ().color = Color.green;
                StartCoroutine(delayAfterAnswers(false, _btn.GetComponent<AnswerButton>().Player1));
                activeAllAnswerButtons (false);
			}
            else
            {
				_btn.GetComponent<Image> ().color = Color.red;
				disableAnswerButtons (_btn.GetComponent<AnswerButton> ().Player1);
                if (countBadAnswers > 0)
                {
                    countBadAnswers = 0;
                    StartCoroutine(delayAfterAnswers(true));

                }
                else
                {
                    countBadAnswers++;
                }
			}
		}			
	}
    public void resetSongNumber()
    {
        songNumber = 0;
    }
    private IEnumerator delayAfterAnswers(bool draw, bool isPlayer1 = false)
    {
        AudioSrc.Stop();
        songNumber++;
        CanAnswer = false;
        float waitTime = 0.5f;
        yield return new WaitForSeconds(waitTime);
        if (!draw)
        {
            if (isPlayer1)
            {
                fightPointsForPlayer1++;
                Battle1Score.text = fightPointsForPlayer1.ToString();
            }
            else
            {
                fightPointsForPlayer2++;
                Battle2Score.text = fightPointsForPlayer2.ToString();
            }
        }
        BeginFight();
        

    }

    private void disableAnswerButtons(bool player1)
	{
		if (player1) {
			foreach (Button btn in Answer_Buttons1) {
				btn.enabled = false;
			}
		} else {
			foreach (Button btn in Answer_Buttons2) {
				btn.enabled = false;
			}
		}
	}

	private void activeAllAnswerButtons(bool active){
		foreach (Button btn in Answer_Buttons1) {
			btn.enabled = active;
		}
		foreach (Button btn in Answer_Buttons2) {
			btn.enabled = active;
		}
	}
	private void ResetAnswerButtons()
	{
		foreach (Button btn in Answer_Buttons1) {
			btn.enabled = true;
			btn.GetComponent<Image> ().color = originButtonColor;
		}
		foreach (Button btn in Answer_Buttons2) {
			btn.enabled = true;
			btn.GetComponent<Image> ().color = originButtonColor;
		}
	}
	private void BeginButton_Click()
	{
        CanAnswer = false;
		VersusPanel.SetActive (false);
		PlayModeHolder.SetActive (true);
        BeginFight();     
    }

	private void SetAnswers()
	{
        countBadAnswers = 0;
        ResetAnswerButtons ();
		AudioSrc.clip = Songs [songNumber].audio;
		int correct = Random.Range (0, 4);
		int falseAnswerCounter = 0;
		for (int i = 0; i < 4; i++) {
			if(correct == i)
			{
				Answer_Buttons1 [i].GetComponent<AnswerButton> ().SetButton (Songs[songNumber].CorrectAnswer, false);
				Answer_Buttons2 [i].GetComponent<AnswerButton> ().SetButton (Songs[songNumber].CorrectAnswer, false);

			}
			else
			{
				Answer_Buttons1 [i].GetComponent<AnswerButton> ().SetButton (Songs[songNumber].FalseAnswers[falseAnswerCounter], true);
				Answer_Buttons2 [i].GetComponent<AnswerButton> ().SetButton (Songs[songNumber].FalseAnswers[falseAnswerCounter], true);
				falseAnswerCounter++;
			}
		}
	}

	private IEnumerator CountTime()
	{
		activeTimerText (true);
		int i = 0;
		int time = 3;
		while (i < 3) {
			setTimerText (time);
			yield return new WaitForSeconds (1f);
			time--;
			i++;
		}
		activeTimerText (false);
		CanAnswer = true;
		AudioSrc.Play ();
		
	}



	private void setTimerText(int i)
	{
		Timer1.text = i.ToString ();
		Timer2.text = i.ToString ();
	}
	private void activeTimerText(bool active)
	{
		Timer1.gameObject.SetActive (active);
		Timer2.gameObject.SetActive (active);
	}

}
