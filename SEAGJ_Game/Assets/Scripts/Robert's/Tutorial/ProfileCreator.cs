using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

public class ProfileCreator : MonoBehaviour
{
    RectTransform selfTransform;
    [SerializeField]
    RectTransform nameInputField;
    [SerializeField]
    InputField nameText;
    bool didNameError;
    [SerializeField]
    RectTransform genderSelectField;
    [SerializeField]
    Toggle isMaleToggle;
    [SerializeField]
    ToggleGroup genderToggle;
    bool didGenderError;
    [SerializeField]
    Button confirmButton;

    public static UnityEvent onNewProfile = new UnityEvent();

    void Awake()
    {

        selfTransform = GetComponent<RectTransform>();
        selfTransform.anchoredPosition = new Vector2(0,720);
        confirmButton.onClick.AddListener(CheckInput);
        onNewProfile.AddListener(DropProfile);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene,LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name != "GMCIntro")
        {
            gameObject.SetActive(false);
        }
    }

    void CheckInput()
    {
        if(nameText.text.Length == 0)
        {
            if(!didNameError)
            {
                nameInputField.DOAnchorPosY(-64,1.0f).SetEase(Ease.OutCubic).SetRelative(true);
                didNameError = true;
            }
        }
        else
        {
            if(didNameError)
            {
                nameInputField.DOAnchorPosY(64,1.0f).SetEase(Ease.OutCubic).SetRelative(true);
                didNameError = false;
            }
        }
        if(!genderToggle.AnyTogglesOn())
        {
            if(!didGenderError)
            {
                genderSelectField.DOAnchorPosY(-64,1.0f).SetEase(Ease.OutCubic).SetRelative(true);
                didGenderError = true;
            }
        }
        else
        {
            if(didGenderError)
            {
                genderSelectField.DOAnchorPosY(64,1.0f).SetEase(Ease.OutCubic).SetRelative(true);
                didGenderError = false;
            }
        }

        if(didNameError == false)
        {
            if(didGenderError == false)
            {
                LocalDataManager.instance.SetGender(isMaleToggle.isOn ? 0 : 1);
                LocalDataManager.instance.SetName(nameText.text);
                GetComponent<LoadScene>().StartLoadSceneSequence();
            }
        }
    }

    void DropProfile()
    {
        selfTransform.DOAnchorPosY(0,1.5f).SetEase(Ease.OutBounce);
    }
}
