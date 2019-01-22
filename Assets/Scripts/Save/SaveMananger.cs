using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMananger : MonoBehaviour {

    public static SaveMananger instance;
    public SaveState state;
    private string save = "MusicRivals4";
    public Category defaultCategory;

    public void Awake()
    {
        instance = this;
        Load();
    }

    public void Save(int Coins)
    {
        state.Coins = Coins;
        PlayerPrefs.SetString(save, Helper.Serialize<SaveState>(state));
    }
    public void Save()
    {
        PlayerPrefs.SetString(save, Helper.Serialize<SaveState>(state));
    }


    public void Load()
    {
        if(PlayerPrefs.HasKey(save))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString(save));
        }
        else
        {
            state = new SaveState();
            state.mCategoryList = new List<string>();
            state.mCategoryList.Add(defaultCategory.CategoryName);
            Save();
        }
        
    }


}
