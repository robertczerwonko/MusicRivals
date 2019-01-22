using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Game : MonoBehaviour {

	[Header("UI Elements")]
	public Button StartBTN;

	void Start()
	{
		StartBTN.onClick.AddListener(() => StartBTN_click());
	}

	private void StartBTN_click()
	{
        active(false);
		Game_Manager.instance.BeginGame ();
	}

    public void active(bool active)
    {
        gameObject.SetActive(active);
    }
}
