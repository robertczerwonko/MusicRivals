using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setup_Game : MonoBehaviour {

	[Header("Setup Holder")]
	public GameObject SetupHolder;
	public Button CategoryPanelButton;
	public Button AddPlayerName;
	public Button StartButton;
	public GameObject ScrollViewContent; 
	public InputField PlayerNameInput;
	public GameObject PlayerNamePrefab;

	[Header("Category Panel")]
	public GameObject CategoryPanel;
	public Button[] CategoryButtons;
	public Button ConfirmButton;
	public Text ChoosenCategoryText;
    public Text coinsText;


	// List of Players from setup holder
	private List<string> Players;
    private List<GameObject> PlayersHost;
    private int playersAlreadyOnList = 0;
    private int PlayerHostIndex = 0;
	// Choosen category from category panel
	private Category ChoosenCategory;

	void Start()
	{
		Players = new List<string> ();
        PlayersHost = new List<GameObject>();
        //set default category
        ChoosenCategory = CategoryButtons[0].GetComponent<Category_Button>().cat;
		setSetupHolderButtons ();
		setCategoryPanelButtons ();
	}


	public void BeginSetup()
	{
        if (Players != null)
        {
            Players.Clear();           
        }
        if(PlayersHost.Count > 0)
        {
            playersAlreadyOnList = PlayersHost.Count;
            foreach(GameObject gobj in PlayersHost)
            {
                gobj.GetComponent<Text>().text = "";
            }
            PlayerHostIndex = 0;
        }
		if(!SetupHolder.activeSelf)
			SetupHolder.SetActive (true);
        coinsText.text = Game_Manager.instance.coins.ToString();
	}

	private void setSetupHolderButtons()
	{
		CategoryPanelButton.onClick.AddListener(() => CategoryPanelButton_Click());
		AddPlayerName.onClick.AddListener (() => AddPlayerName_Click ());
		StartButton.onClick.AddListener (() => StartButton_Click ());
	}

	private void setCategoryPanelButtons()
	{
		foreach (Button btn in CategoryButtons) {
			btn.onClick.AddListener(() => CategoryButton_Click(btn.GetComponent<Category_Button>().cat));
		}
		ConfirmButton.onClick.AddListener (() => ConfirmButton_Click ());
	}

	private void StartButton_Click()
	{
		string[] p = new string[Players.Count];
		for (int i = 0; i < Players.Count; i++)
			p [i] = Players [i];
		if (Players.Count >= 2) {
            SetupHolder.SetActive(false);
			//gameObject.SetActive (false);
			Game_Manager.instance.SetData (p, ChoosenCategory);
		}
	}
	private void ConfirmButton_Click()
	{
        coinsText.color = Color.white;
        CategoryPanel.SetActive (false);
		SetupHolder.SetActive (true);
	}
	private void CategoryButton_Click(Category c)
	{
        if(SaveMananger.instance.state.mCategoryList.Contains(c.CategoryName))
        {
            ChoosenCategory = c;
            ChoosenCategoryText.text = c.CategoryName;
        }
        else
        {
            if(Game_Manager.instance.coins >= 5)
            {
                Game_Manager.instance.setCoins(-5);
                coinsText.text = Game_Manager.instance.coins.ToString();
                EventCanvas.instance.activeCategoryBuy(c);
                SaveMananger.instance.state.mCategoryList.Add(c.CategoryName);
                SaveMananger.instance.Save();
                ChoosenCategory = c;
                ChoosenCategoryText.text = c.CategoryName;
            }
            else
            {
                coinsText.color = Color.red;
            }
        }

        //OBSLUGA KUPOWANIA??? JESZCZE TO THINK;
	}
	private void CategoryPanelButton_Click()
	{
		SetupHolder.SetActive (false);
		CategoryPanel.SetActive (true);
	}

	private void AddPlayerName_Click ()
	{
		string pName = PlayerNameInput.text;
		if (pName.Length >= 3 && pName.Length < 10 && NoSpaceInName (pName) ) {
            if (PlayerHostIndex < playersAlreadyOnList)
            {
                PlayersHost[PlayerHostIndex].GetComponent<Text>().text = pName;
                PlayerHostIndex++;
            }
            else
            {
                GameObject temp = Instantiate(PlayerNamePrefab);
                temp.GetComponent<Text>().text = pName;
                temp.transform.SetParent(ScrollViewContent.gameObject.transform, false);
                PlayersHost.Add(temp);
                
                
            }
            Players.Add(pName);
            PlayerNameInput.text = "";
        }
	}

	private bool NoSpaceInName(string word)
	{
		for (int i = 0; i < word.Length; i++) {
			if (string.Equals(word[i]," "))
				return false;
		}

		return true;
	}



}
