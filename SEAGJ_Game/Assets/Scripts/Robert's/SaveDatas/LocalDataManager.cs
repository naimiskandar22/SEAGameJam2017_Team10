using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataManager : MonoBehaviour
{

    public static LocalDataManager instance { get; private set; }

    [SerializeField]
    bool clearData;
    string playerName;
    int gender;
    int reputation;
    int date;
    int cash;
    int doneTutorial;

    [SerializeField]
    bool debugProfile;

    public static string saveDataNameKey = "KEY_NAME";
    public static string saveDataGenderKey = "KEY_GENDER";
    public static string saveDataReputationKey = "KEY_REPUTATION";
    public static string saveDataDateKey = "KEY_DATE";
    public static string saveDataCashKey = "KEY_CASH";
    public static string saveDataDoneTutorialKey = "KEY_TUTORIAL";

    // Use this for initialization
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(debugProfile)
        {
            PlayerPrefs.SetInt(saveDataDoneTutorialKey,1);
        }

        if(clearData)
        {
            SetFreshData();
        }
        else
        {
            GetData();
        }
    }

    void SetFreshData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString(saveDataNameKey,"Mak Cun");
        PlayerPrefs.SetInt(saveDataGenderKey,0);
        PlayerPrefs.SetInt(saveDataDateKey,0);
        PlayerPrefs.SetInt(saveDataCashKey,1000);
        PlayerPrefs.SetInt(saveDataReputationKey,0);
        PlayerPrefs.SetInt(saveDataDoneTutorialKey,0);
        GetData();
    }

    void GetData()
    {
        playerName = PlayerPrefs.GetString(saveDataNameKey,"Mak Cun");
        gender = PlayerPrefs.GetInt(saveDataGenderKey,0);
        reputation = PlayerPrefs.GetInt(saveDataReputationKey,0);
        date = PlayerPrefs.GetInt(saveDataDateKey,0);
        cash = PlayerPrefs.GetInt(saveDataCashKey,1000);
        doneTutorial = PlayerPrefs.GetInt(saveDataDoneTutorialKey,0);
    }

    //Name
    public void SetName(string newName)
    {
        playerName = newName;
        PlayerPrefs.SetString(saveDataNameKey,playerName);
    }

    public string GetName()
    {
        return playerName;
    }

    //Gender
    public void SetGender(int newGender)
    {
        gender = newGender;
        PlayerPrefs.SetInt(saveDataGenderKey,gender);
    }

    public int GetGender()
    {
        return gender;
    }

    //Reputation
    public void SetReputation(int newReputation)
    {
        reputation = newReputation;
        PlayerPrefs.SetInt(saveDataReputationKey,reputation);
    }

    public int GetReputation()
    {
        return reputation;
    }

    //Date
    public void SetDate(int newDate)
    {
        date = newDate;
        PlayerPrefs.SetInt(saveDataDateKey,date);
    }

    public int GetDate()
    {
        return date;
    }

    //Cash
    public void SetCash(int newCash)
    {
        cash = newCash;
        PlayerPrefs.SetInt(saveDataCashKey,cash);
    }

    public int GetCash()
    {
        return cash;
    }

    //Tutorial
    public void SetTutorial(int newTutorial)
    {
        doneTutorial = newTutorial;
        PlayerPrefs.SetInt(saveDataDoneTutorialKey,doneTutorial);
    }

    public int GetTutorial()
    {
        return doneTutorial;
    }
}
