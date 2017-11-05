using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAudio : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    AudioSource myAudio;

    // Use this for initialization


    public void OnPointerDown(PointerEventData eventData)
    {
        myAudio.PlayOneShot(myAudio.GetComponent<SFXList>().clickDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        myAudio.PlayOneShot(myAudio.GetComponent<SFXList>().clickUp);

    }
}
