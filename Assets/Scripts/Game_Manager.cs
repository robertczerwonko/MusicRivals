using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance;
    public int coins = 0;
	void Awake()
	{
		instance = this;
	}

    void Start()
    {
        SaveMananger.instance.Load();
        coins = SaveMananger.instance.state.Coins;
        coins += 10; 
    }
	[Header("Game Elements")]
	public Start_Game StartGame;
	public Setup_Game SetupGame;
	public Main_Game MainGame;
	public Play_Mode PlayMode;
	public End_Game EndGame;

	string[] players;
	Category CurrentCategory;

    public void setCoins(int value)
    {
        coins += value;
        SaveMananger.instance.Save(coins);
    }

    public void ActiveStartGameScreen()
    {
        StartGame.active(true);
    }

	public void BeginGame()
	{
		SetupGame.BeginSetup ();
	}

	public void SetData(string[] _players, Category mCat)
	{
		players = _players;
		CurrentCategory = mCat;
		MainGame.StartMainGame (players, CurrentCategory);
	}
		

	public void FinishGame(List<string> winner)
	{
        string stringOfWinners = "WINNERS!!! \n";
        foreach(string str in winner)
        {
            stringOfWinners += str + ", ";
        }
        int coinsEarn = 5;
        EventCanvas.instance.activeCoinReward(coinsEarn);
        coins += coinsEarn;

        SaveMananger.instance.Save(coins);
		EndGame.setWinnerText(stringOfWinners);
	}

}
