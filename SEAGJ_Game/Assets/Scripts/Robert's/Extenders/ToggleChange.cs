using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChange : MonoBehaviour
{

    Toggle myToggle;
    Image myImage;

    void Start()
    {
        myToggle = GetComponent<Toggle>();
        myImage = GetComponent<Image>();
        myImage.color = Color.grey;
        myToggle.onValueChanged.AddListener(ToggleSFX);
    }

    void ToggleSFX(bool newValue)
    {
        if(newValue)
        {
            myImage.color = Color.white;
        }
        else
        {
            myImage.color = Color.grey;
        }
    }
}
