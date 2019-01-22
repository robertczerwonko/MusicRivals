using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCanvas : MonoBehaviour {

    public static EventCanvas instance;
    void Awake()
    {
        instance = this;
    }


    [Header("Coin reward")]
    public GameObject cr_coinPanel;
    public Text cr_coinScoreText;
    public Button cr_ContinuteButton;


    [Header("Category buy")]
    public GameObject cb_buyPanel;
    public Text cb_coinScoreText;
    public Text cb_categoryName;
    public Button cb_ContinuteButton;

    void Start()
    {
        cr_ContinuteButton.onClick.AddListener(()=>cr_ContinuteButton_onClick());
        cb_ContinuteButton.onClick.AddListener(() => cb_ContinuteButton_onClick());
    }

    public void activeCoinReward(int value)
    {

        cr_coinScoreText.text = "+" + value.ToString();
        cr_coinPanel.SetActive(true);

    }

    public void activeCategoryBuy(Category cat)
    {
        cb_buyPanel.SetActive(true);
        cb_categoryName.text = cat.CategoryName;
        cb_coinScoreText.text = 5.ToString();
    }


    #region onClicks
    private void cr_ContinuteButton_onClick()
    {
        cr_coinPanel.SetActive(false);
    }
    private void cb_ContinuteButton_onClick()
    {
        cb_buyPanel.SetActive(false);
    }
    #endregion

}
