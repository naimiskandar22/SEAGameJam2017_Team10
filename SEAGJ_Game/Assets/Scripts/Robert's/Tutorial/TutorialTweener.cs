using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialTweener : MonoBehaviour {

    [SerializeField]
    RectTransform leftTrans, rightTrans, upTrans, bottomTrans;
    float XFloat, YFloat;

	// Use this for initialization
	void Start () {
        XFloat = 1280f;
        YFloat = 720f;
        gameObject.SetActive(false);
    }
	
    public void TryTween()
    {
        leftTrans.anchoredPosition += new Vector2(-XFloat,0);
        rightTrans.anchoredPosition += new Vector2(XFloat,0);
        upTrans.anchoredPosition += new Vector2(0,-YFloat);
        bottomTrans.anchoredPosition += new Vector2(0,YFloat);
        leftTrans.DOAnchorPosX(XFloat,1.0f).SetRelative(true).SetDelay(0.2f);
        rightTrans.DOAnchorPosX(-XFloat,1.0f).SetRelative(true).SetDelay(0.2f);
        upTrans.DOAnchorPosY(YFloat,1.0f).SetRelative(true).SetDelay(0.2f);
        bottomTrans.DOAnchorPosY(-YFloat,1.0f).SetRelative(true).SetDelay(0.2f);
    }
}
