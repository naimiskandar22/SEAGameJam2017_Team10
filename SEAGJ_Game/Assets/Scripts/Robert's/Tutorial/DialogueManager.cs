using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using DG.Tweening;
//using I2.Loc;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance { get; private set; }

    [Header("Dialogue Objects")]
    [SerializeField]
    private GraphicRaycaster raycaster;
    [SerializeField]
    private Image background;
    [SerializeField]
    private CanvasGroup backgroundGroup;
    [SerializeField]
    private Image portraitLeft;
    [SerializeField]
    private Image portraitRight;
    [SerializeField]
    private Text speakerName;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Image dialogueNext;
    [SerializeField]
    private bool dialogueMoveOn;

    [Header("Dialogues Database")]
    [SerializeField]
    private Dialogue[] dialogues;

    [Header("Localization")]
    //[SerializeField]
    //private Localize nameLocalize;
    //[SerializeField]
    // private Localize dialogueLocalize;

    // Store current dialogue data.
    private Dialogue currentDialogue;
    private Sentence currentSentence;
    private int currentSentenceIndex;

    // For dialogue end
    public delegate void DialogueEndEvent();
    public DialogueEndEvent OnDialogueEnd;

    // For Bolaway 
    private bool changeStateAble;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene,LoadSceneMode mode)
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "GMCMap":          
                {
                    gameObject.SetActive(true);
                    if(PlayerPrefs.GetInt(LocalDataManager.saveDataDoneTutorialKey,0) == 0)
                    {
                        switch(TutorialManager.instance.GetState())
                        {
                            case TutorialManager.Tutorial_State.Start:
                                TutorialManager.instance.SetState(TutorialManager.Tutorial_State.Start);
                                StartDialogue("Tutorial - Intro",1.0f);
                                break;
                        }
                    }
                    break;
                }
            default:
                {
                    gameObject.SetActive(false);
                    break;
                }
        }
    }

    /*void OnLevelWasLoaded(int _scene)
    {
        //StartDialogue("Tutorial 1");

        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                if (SaveDataManager.instance.GetSaveData().playerID == -1) 
                {
                    switch (TutorialManager.instance.GetState())
                    {
                        case TutorialManager.Tutorial_State.Start:
                            TutorialManager.instance.SetState(TutorialManager.Tutorial_State.Start);
                            StartDialogue("Tutorial - Naming", 1.0f);
                            break;
                        case TutorialManager.Tutorial_State.Naming:
                            break;
                        case TutorialManager.Tutorial_State.BasicTalk:
                            break;
                    }
                }
                break;
        }
    
        //switch(SceneManager.GetActiveScene().name)
        //{
        //    case "U_Lv2":
        //        if (!SaveDataManager.singleton.saveData.underworldStageCompleted[1])
        //            StartCoroutine(StartDialogueWithDelay("Fight Bomoh", 1f));
        //        break;

        //    case "U_Lv3":
        //        if (!SaveDataManager.singleton.saveData.underworldStageCompleted[3])
        //            StartCoroutine(StartDialogueWithDelay("Fight Taoist", 1f));
        //        break;

        //    case "U_Lv4":
        //        if (!SaveDataManager.singleton.saveData.underworldStageCompleted[5])
        //            StartCoroutine(StartDialogueWithDelay("Fight Vampy", 1f));
        //        break;

        //    case "U_Lv5":
        //        if (!SaveDataManager.singleton.saveData.underworldStageCompleted[6])
        //            StartCoroutine(StartDialogueWithDelay("Fight Overlord", 1f));
        //        break;

        //    case "H_Lv2":
        //        if (!SaveDataManager.singleton.saveData.hellfireStageCompleted[1])
        //            StartCoroutine(StartDialogueWithDelay("Fight Majinn", 1f));
        //        break;

        //    case "H_Lv5":
        //        if (!SaveDataManager.singleton.saveData.hellfireStageCompleted[4])
        //            StartCoroutine(StartDialogueWithDelay("Fight Firelord", 1f));
        //        break;

        //    case "H_Lv6":
        //        if (!SaveDataManager.singleton.saveData.hellfireStageCompleted[5])
        //            StartCoroutine(StartDialogueWithDelay("Fight Worldeater", 1f));
        //        break;

        //    case "H_Lv8":
        //        if (!SaveDataManager.singleton.saveData.hellfireStageCompleted[7])
        //            StartCoroutine(StartDialogueWithDelay("Fight Lilith", 1f));
        //        break;

        //    case "H_Lv9":
        //        if (!SaveDataManager.singleton.saveData.hellfireStageCompleted[8])
        //            StartCoroutine(StartDialogueWithDelay("Fight Satan", 1f));
        //        break;

        //    case "E_Lv2":
        //        if (!SaveDataManager.singleton.saveData.earthStageCompleted[1])
        //            StartCoroutine(StartDialogueWithDelay("Fight Buster", 1f));
        //        break;

        //    case "E_Lv3":
        //        if (!SaveDataManager.singleton.saveData.earthStageCompleted[2])
        //            StartCoroutine(StartDialogueWithDelay("Fight Kungfusensei", 1f));
        //        break;

        //    case "E_Lv6":
        //        if (!SaveDataManager.singleton.saveData.earthStageCompleted[5])
        //            StartCoroutine(StartDialogueWithDelay("Fight Madscientist", 1f));
        //        break;

        //    case "E_Lv7":
        //        if (!SaveDataManager.singleton.saveData.earthStageCompleted[6])
        //            StartCoroutine(StartDialogueWithDelay("Fight Petmaster", 1f));
        //        break;

        //    case "E_Lv8":
        //        if (!SaveDataManager.singleton.saveData.earthStageCompleted[7])
        //            StartCoroutine(StartDialogueWithDelay("Fight Spacecadet", 1f));
        //        break;
        //}

    }*/

    void OpenDialogueCanvas()
    {
        // Start detecting clicks for dialogue canvas.
        raycaster.enabled = true;
        background.gameObject.SetActive(true);
        // Activate the dialogue parent object.
        backgroundGroup.DOFade(1.0f,1.0f);
    }

    void CloseDialogueCanvas()
    {
        // Stop detecting clicks for dialogue canvas.
        raycaster.enabled = false;

        // Deactivate the dialogue parent object.
        backgroundGroup.DOFade(0f,1.0f).OnComplete(()=>{ background.gameObject.SetActive(false); }) ;
    }

    void SetPortrait()
    {
        if(currentSentence.portraitAtRight)
        {
            portraitRight.gameObject.SetActive(true);
            portraitRight.sprite = currentSentence.speakerPortrait;
            portraitLeft.gameObject.SetActive(false);
        }
        else
        {
            portraitLeft.gameObject.SetActive(true);
            portraitLeft.sprite = currentSentence.speakerPortrait;
            portraitRight.gameObject.SetActive(false);
        }
    }

    void SetDialogueText()
    {
        DOTween.Kill(dialogueNext);
        dialogueNext.DOFade(0,0f);
        speakerName.text = currentSentence.speakerName;
        //dialogueText.text = currentSentence.sentenceText;

        StartCoroutine(AnimateText(currentSentence.sentenceText));

        // Localization.
        //nameLocalize.SetTerm(currentSentence.speakerName);
        //dialogueLocalize.SetTerm(currentDialogue.dialogueId + "." + (currentSentenceIndex + 1));
    }

    void EndDialogue()
    {
        Time.timeScale = 1.0f;
        CloseDialogueCanvas();
        if(OnDialogueEnd != null) OnDialogueEnd.Invoke();
    }

    Dialogue GetDialogueWithId(string _dialogueId)
    {
        for(int i = 0;i < dialogues.Length;i++)
        {
            if(dialogues[i].dialogueId == _dialogueId)
                return dialogues[i];
        }

        Debug.Log("No dialogue with id : " + _dialogueId + " is found.");
        return null;
    }

    public IEnumerator StartDialogueWithDelay(string _dialogueId,float _delay,float timeScale)
    {
        yield return new WaitForSeconds(_delay);

        changeStateAble = true;
        // Save current dialogue info.
        currentDialogue = GetDialogueWithId(_dialogueId);
        currentSentenceIndex = 0;
        currentSentence = currentDialogue.sentencesInDialogue[currentSentenceIndex];

        // Stop the time scale.
        Time.timeScale = timeScale;

        // Open dialogue canvas.
        OpenDialogueCanvas();

        // Set the portrait for current sentence.
        SetPortrait();

        // Set the text for current sentence.
        SetDialogueText();
    }

    public void StartDialogue(string _dialogueId,float timeScale)
    {
        changeStateAble = true;
        // Save current dialogue info.
        currentDialogue = GetDialogueWithId(_dialogueId);
        currentSentenceIndex = 0;
        currentSentence = currentDialogue.sentencesInDialogue[currentSentenceIndex];
        // Stop the time scale.
        Time.timeScale = timeScale;

        // Open dialogue canvas.
        OpenDialogueCanvas();

        // Set the portrait for current sentence.
        SetPortrait();

        // Set the text for current sentence.
        SetDialogueText();
    }

    public void StartDialogueWithoutChangeState(string _dialogueId,float timeScale)
    {
        changeStateAble = false;

        // Save current dialogue info.
        currentDialogue = GetDialogueWithId(_dialogueId);
        currentSentenceIndex = 0;
        currentSentence = currentDialogue.sentencesInDialogue[currentSentenceIndex];
        // Stop the time scale.
        Time.timeScale = timeScale;

        // Open dialogue canvas.
        OpenDialogueCanvas();

        // Set the portrait for current sentence.
        SetPortrait();

        // Set the text for current sentence.
        SetDialogueText();
    }

    public void NextSentence()
    {
        // Go to next sentence. 
        currentSentenceIndex++;

        // If no more sentences for the dialogue
        if(currentSentenceIndex == currentDialogue.sentencesInDialogue.Length)
        {
            if(changeStateAble)
            {
                TutorialManager.instance.SetStateFinish(TutorialManager.instance.GetState(),true);
            }
            EndDialogue();
            return;
        }

        currentSentence = currentDialogue.sentencesInDialogue[currentSentenceIndex];

        // Set the portrait for current sentence.
        SetPortrait();

        // Set the text for current sentence.
        SetDialogueText();
    }

    IEnumerator AnimateText(string fullText)
    {
        int i = 0;
        dialogueText.text = "";
        while(i < fullText.Length)
        {
            dialogueText.text += fullText[i++];
            yield return new WaitForSeconds(0.01F);
        }
        dialogueNext.DOFade(0.8f,0.7f).SetLoops(-1,LoopType.Yoyo);
        dialogueMoveOn = !dialogueMoveOn;
    }

    public void Next()
    {
        dialogueMoveOn = !dialogueMoveOn;

        if(dialogueMoveOn)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence.sentenceText;
            dialogueNext.DOFade(0.8f,0.7f).SetLoops(-1,LoopType.Yoyo);
        }
        else
            NextSentence();
    }

}

[Serializable]
public class Dialogue
{
    public string dialogueId;
    public Sentence[] sentencesInDialogue;
}

[Serializable]
public class Sentence
{
    public string speakerName;
    public Sprite speakerPortrait;
    [TextArea(1,2)]
    public string sentenceText;
    public bool portraitAtRight;
}
