using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Game : MonoBehaviour {

	[Header("Main Game Elements")]
	[Space(10)]
	public Button StartRoundButton;
	[Header("TEAM 1")]
	public Text Team1_ScoreText;
	public GameObject Team1_Players;
	[Header("TEAM 2")]
	public Text Team2_ScoreText;
	public GameObject Team2_Players;
	[Space(10)]
	public GameObject TextPrefab;
	[Space(10)]
	[Header("Play mode")]
	public Play_Mode _PlayMode;
    private List<GameObject> PlayerNameHosts;


    float team1_scoreValue;
	float team2_scoreValue;
	string[] players;
	List<string> Team1;
	List<string> Team2;
	Category CurrentCategory;
	int pointsPeak;
    bool gameFinished;
	int nowPlayT1;
	int nowPlayT2;
	void Start()
	{
		StartRoundButton.onClick.AddListener (() => StartRoundButton_Click ());
        PlayerNameHosts = new List<GameObject>();

    }
	public void StartMainGame(string[] p,Category c)
	{
		gameObject.SetActive (true);
        if (players != null)
        {
            resetMainGame();
        }

        if (players != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                Debug.Log("PRZED" + players[i]);
            }
        }
        players = p;
        for(int i = 0; i < players.Length; i++)
        {
            Debug.Log("PO"+players[i]);
        }
		CurrentCategory = c;
		pointsPeak = CurrentCategory.Songs.Count;
		SetTeams ();
	}

    private void resetMainGame()
    {
        //System.Array.Clear(players, 0, players.Length);
        //players = null;
        //Debug.Log("USTAWIAM NA NULLA????");
        //Team1.Clear();
        //Team2.Clear();
        //team1_scoreValue = 0;
        //team2_scoreValue = 0;
        //Team2_ScoreText.text = 0.ToString();
        //Team1_ScoreText.text = 0.ToString();
        //foreach(GameObject gObj in PlayerNameHosts)
        //{
        //    Destroy(gObj);
        //}
        //PlayerNameHosts = new List<GameObject>();

    }
	private void StartRoundButton_Click()
	{
		gameObject.SetActive (false);
		setNowPlay ();
		_PlayMode.PresentPlayers (Team1 [nowPlayT1], Team2 [nowPlayT2],CurrentCategory);
	}


	public void FinishedRound(bool draw, bool winnerP1 = false,int player1PointsFromBattle = 0, int player2PointsFromBattle = 0)
	{
       
        if (!draw)
        {
            RoundFinished(winnerP1, player1PointsFromBattle, player2PointsFromBattle);
        }

        pointsPeak -= 3;
        if (pointsPeak <= 0)
        {
            _PlayMode.resetSongNumber();
            List<string> winner = team1_scoreValue >= team2_scoreValue ? Team1 : Team2;
            Game_Manager.instance.FinishGame(winner);
            Debug.Log("GAME FINISHED");
        }
        else
            gameObject.SetActive(true);


    }

    private void RoundFinished(bool p1IsWinner,int valueForT1, int valueForT2)
	{
        if (p1IsWinner && valueForT1 == 3)
        
            team1_scoreValue++;

        if (!p1IsWinner && valueForT2 == 3)
            team2_scoreValue++;


        team1_scoreValue += valueForT1;
        Team1_ScoreText.text = team1_scoreValue.ToString();
        team2_scoreValue += valueForT2;
        Team2_ScoreText.text = team2_scoreValue.ToString();

	}

	private void setNowPlay()
	{        
        nowPlayT1 = (nowPlayT1 - 1) < 0 ? Team1.Count - 1 : nowPlayT1 - 1;
		nowPlayT2 = (nowPlayT2 - 1) < 0 ? Team2.Count - 1 : nowPlayT2 - 1;

    }
	

	private void SetTeams()
	{
		Team1 = new List<string> ();
		Team2 = new List<string> ();
		int[] positions = new int[players.Length];
		//Populate array with numbers from 0 to lenght of Array
		for (int i = 0; i < players.Length; i++) {
			positions [i] = i;
		}

		//Suffle positions
		for (int i = 0; i < players.Length * 10; i++) {
			int a = Random.Range (0, positions.Length);
			int b = Random.Range (0, positions.Length);
			int temp = positions [a];
			positions [a] = positions [b];
			positions [b] = temp;
		}


		for (int j = 0; j < players.Length; j++) {
			if (j % 2 == 0)
				Team1.Add (players [positions [j]]);
			else
				Team2.Add (players [positions [j]]);
		}

		nowPlayT1 = Team1.Count;
		nowPlayT2 = Team2.Count;
        ShowPlayersList ();
	}


    
	private void ShowPlayersList()
	{
        PlayerNameHosts = new List<GameObject>();

        foreach (string str in Team1) {
			GameObject temp = Instantiate (TextPrefab);
            PlayerNameHosts.Add(temp);
            temp.GetComponent<Text> ().text = str;
			temp.transform.SetParent (Team1_Players.gameObject.transform, false);           

        }

		foreach (string str in Team2) {
			GameObject temp = Instantiate (TextPrefab);
            PlayerNameHosts.Add(temp);
            temp.GetComponent<Text> ().text = str;
			temp.transform.SetParent (Team2_Players.gameObject.transform, false);

        }
	}
}
