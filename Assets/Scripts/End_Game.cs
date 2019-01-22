using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End_Game : MonoBehaviour {

    public Text winnerTeamText;
    public Button continueBTN;

    void Start()
    {
        //gameObject.SetActive(false);
        continueBTN.onClick.AddListener(() => continueBTN_onClick());
    }

    public void setWinnerText(string team)
    {
        Debug.Log("KONCZYMY?");
        gameObject.SetActive(true);       
        winnerTeamText.text = team;
    }


    private void continueBTN_onClick()
    {
        gameObject.SetActive(false);
        Game_Manager.instance.ActiveStartGameScreen();
        SceneManager.LoadScene(0);
    }
}
