using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour
{
    Button myButton;

    void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(CheckSaveFile);
    }

    void CheckSaveFile()
    {
        if(PlayerPrefs.GetInt(LocalDataManager.saveDataDoneTutorialKey,0)==0)
        {
            ProfileCreator.onNewProfile.Invoke();
        }
        else
        {
            GetComponent<LoadScene>().StartLoadSceneSequence();
        }
    }
}
