using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{

    public static TutorialManager instance { get; private set; }

    public enum Tutorial_State { Start, OpenShop, HintAddPrice, AddingPrice, AddedPrice, BuyingStuff, BoughtStuff, ClosedShop, CheckBalance, Checkingbalance, CheckDaily, CheckingDaily,FinishTutorial };

    [HideInInspector]
    public Tutorial_State t_State;

    [SerializeField]
    GameObject openingPanel;
    [SerializeField]
    GameObject addingPanel;
    [SerializeField]
    GameObject buyingPanel;
    [SerializeField]
    GameObject closingPanel;
    [SerializeField]
    GameObject checkingPanel;
    [SerializeField]
    GameObject dailyPanel;

    public bool[] stateFinished;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        stateFinished = new bool[Enum.GetValues(typeof(Tutorial_State)).Length];
        Invoke("VerifyTutorial",1.0f);
    }


    // Update is called once per frame
    void Update()
    {
        /*if(t_State == Tutorial_State.SkillTraining)
        {
            if(SkillManager.instance.usedSkill)
            {
                SetStateFinish(t_State,true);
            }
        }*/
    }

    void VerifyTutorial()
    {
        if(PlayerPrefs.GetInt(LocalDataManager.saveDataDoneTutorialKey,0) == 1)
        {
            instance = null;
            Destroy(gameObject);
        }
    }

    public void SetStateFinish(Tutorial_State currentState,bool stateFinished)
    {
        this.stateFinished[(int)currentState] = stateFinished;
        ChangeState();
    }

    public void SetState(Tutorial_State newState)
    {
        this.t_State = newState;
        //this.stateFinished[(int)newState] = false;
    }

    public Tutorial_State GetState()
    {
        return t_State;
    }

    void ChangeState()
    {
        for(int i = 0;i < stateFinished.Length;i++)
        {
            if(stateFinished[i] == true)
            {
                t_State++;
                break;
            }

        }

        //Debug.Log(t_State);

        switch(t_State)
        {
            case Tutorial_State.OpenShop:
                openingPanel.SetActive(true);
                openingPanel.GetComponent<CanvasGroup>().DOFade(1.0f,1.0f).SetDelay(0.1f);
                openingPanel.GetComponent<TutorialTweener>().TryTween();
                break;
            case Tutorial_State.HintAddPrice:
                openingPanel.GetComponent<CanvasGroup>().DOFade(0.0f,0.7f).OnComplete(() => { openingPanel.SetActive(false); });
                StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Buy Item",0f,1.0f));
                break;
            case Tutorial_State.AddingPrice:
                addingPanel.SetActive(true);
                addingPanel.GetComponent<CanvasGroup>().DOFade(1.0f,1.0f).SetDelay(0.1f);
                addingPanel.GetComponent<TutorialTweener>().TryTween();
                break;
            case Tutorial_State.AddedPrice:
                addingPanel.GetComponent<CanvasGroup>().DOFade(0.0f,0.7f).OnComplete(() => { addingPanel.SetActive(false); });
                StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Close Item",0f,1.0f));
                break;
            case Tutorial_State.BuyingStuff:
                buyingPanel.SetActive(true);
                buyingPanel.GetComponent<CanvasGroup>().DOFade(1.0f,1.0f).SetDelay(0.1f);
                buyingPanel.GetComponent<TutorialTweener>().TryTween();
                break;
            case Tutorial_State.BoughtStuff:
                buyingPanel.GetComponent<CanvasGroup>().DOFade(0.0f,0.7f).OnComplete(() => { buyingPanel.SetActive(false); });
                closingPanel.SetActive(true);
                closingPanel.GetComponent<CanvasGroup>().DOFade(1.0f,1.0f).SetDelay(0.1f);
                closingPanel.GetComponent<TutorialTweener>().TryTween();
                break;
            case Tutorial_State.ClosedShop:
                closingPanel.GetComponent<CanvasGroup>().DOFade(0.0f,0.7f).OnComplete(() => { closingPanel.SetActive(false); });
                StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Check Item",0f,1.0f));
                break;
            case Tutorial_State.CheckBalance:
                checkingPanel.SetActive(true);
                checkingPanel.GetComponent<CanvasGroup>().DOFade(1.0f,1.0f).SetDelay(0.1f);
                checkingPanel.GetComponent<TutorialTweener>().TryTween();
                break;
            case Tutorial_State.Checkingbalance:
                checkingPanel.GetComponent<CanvasGroup>().DOFade(0.0f,0.7f).OnComplete(() => { checkingPanel.SetActive(false); });
                StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Describe GMC",0f,1.0f));
                break;
            case Tutorial_State.CheckDaily:
                dailyPanel.SetActive(true);
                dailyPanel.GetComponent<CanvasGroup>().DOFade(1.0f,1.0f).SetDelay(0.1f);
                dailyPanel.GetComponent<TutorialTweener>().TryTween();
                break;
            case Tutorial_State.CheckingDaily:
                dailyPanel.GetComponent<CanvasGroup>().DOFade(0.0f,0.7f).OnComplete(() => { dailyPanel.SetActive(false); });
                StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Describe Daily",0f,1.0f));
                break;
            case Tutorial_State.FinishTutorial:
                PlayerPrefs.SetInt(LocalDataManager.saveDataDoneTutorialKey,1);
                Destroy(gameObject);
                break;
        }
    }
    /*switch(t_State)
    {
        case Tutorial_State.Naming:
            namingPanel.SetActive(true);
            break;
        case Tutorial_State.BasicTalk:
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Basic Talk",0.0f,1.0f));
            break;
        case Tutorial_State.BasicTrain:
            SceneManager.LoadScene("Tutorial Stadium");
            blackScreen.SetActive(true);
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Basic Train",1.5f,1.0f));
            break;
        case Tutorial_State.MoveTrain:
            blackScreen.SetActive(false);
            if(PlayerPrefs.GetInt(PlayerPrefsKey.selected_swipe_control) == 0)
            {
                tutorialMovePanel.SetActive(true);
                tutorialMovePanel.GetComponent<Animator>().Play("FadeOut",0,0);
            }
            else
            {
                tutorialMovePanel.SetActive(true);
                tutorialMovePanel.GetComponent<Animator>().Play("FadeOut",0,0);
            }
            break;
        case Tutorial_State.KickTrain:
            TutorialSoccerBehaviour.instance.KickStart(Vector3.right);
            break;
        case Tutorial_State.BasicFinish:
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Basic Train - Finish",1.0f,1.0f));
            break;
        case Tutorial_State.BasicDone:
            SceneManager.LoadScene("Menu");
            blackScreen.SetActive(true);
            blackScreen.GetComponent<Animator>().Play("BlackScreen",0,0);
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Basic Train - Done",1.2f,1.0f));
            break;
        case Tutorial_State.SkillTab:
            blackScreen.SetActive(false);
            avatarPanel.SetActive(true);
            break;
        case Tutorial_State.SkillTalk:
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Skill Talk",0.0f,1.0f));
            break;
        case Tutorial_State.SkillChange:
            skillSlotPanel.SetActive(true);
            break;
        case Tutorial_State.SkillChanged:
            SaveDataManager.instance.GetSaveData().avatar1EquippedSkillId[0] = "1";
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Skill Change",0.0f,1.0f));
            break;
        case Tutorial_State.SkillTrain:
            SceneManager.LoadScene("Tutorial Stadium - Skill");
            blackScreen.SetActive(true);
            blackScreen.GetComponent<Animator>().Play("BlackScreen",0,0);
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Skill Train",1.5f,1.0f));
            break;
        case Tutorial_State.SkillTraining:
            TutorialGameManager.instance.AnmStartGame();
            blackScreen.SetActive(false);
            break;
        case Tutorial_State.SkillFinish:
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Skill Done",2.0f,1.0f));
            break;
        case Tutorial_State.SkillDone:
            SceneManager.LoadScene("Menu");
            DebugSettingManager.setting.basicTutorialPass = true;
            blackScreen.SetActive(true);
            blackScreen.GetComponent<Animator>().Play("BlackScreen",0,0);
            StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Practice",1.2f,1.0f));
            break;
        case Tutorial_State.Practice:
            blackScreen.SetActive(false);
            practiceChangePanel.SetActive(true);
            break;
        case Tutorial_State.PracticeStart:
            blackScreen.SetActive(false);
            practiceChangePanel.SetActive(false);
            practiceStartPanel.SetActive(true);
            break;
        case Tutorial_State.FreeGift:
            blackScreen.SetActive(false);
            ShopManager.instance.TutorialFreeGem();
            SetStateFinish(t_State,true);
            break;
        case Tutorial_State.SkipOtherTutorial:
            if(DebugSettingManager.setting.skipOtherTutorial)
            {
                SaveDataManager.instance.GetSaveData().avatar1EquippedSkillId[0] = "1";
                AvatarCustomiseManager.instance.SetSkill1Icon();
                blackScreen.SetActive(false);
                StartCoroutine(DialogueManager.instance.StartDialogueWithDelay("Tutorial - Free Gift",0.5f,1.0f));
            }
            else
                SetStateFinish(t_State,true);
            break;
        case Tutorial_State.FinishTutorial:
            SqlManager.instance.AddNewData();
#if UNITY_ANDROID
            Social.ReportProgress("CgkIr-24xckLEAIQAg",100.0f,(bool success) => { });
#endif
            instance = null;
            Destroy(gameObject);
            break;
    }
}

public void AvatarTabChange()
{
    MenuScreenManager.instance.GoToScreen(1);
    avatarPanel.SetActive(false);
    SetStateFinish(t_State,true);
}

public void SkillSlot()
{
    AvatarCustomiseManager.instance.OpenSkillPanel(0);
    skillSlotPanel.SetActive(false);
    skillChangePanel.SetActive(true);
}

public void SkillChange()
{
    AvatarCustomiseManager.instance.EquipSkill(skillToEquip);
    skillChangePanel.SetActive(false);
    SetStateFinish(t_State,true);
}

public void ChangeMode()
{
    ChallengeUiManager.instance.ChangeMode(1);
    SetStateFinish(t_State,true);
}
*/

    public void ShopClicked()
    {
        if(t_State == Tutorial_State.OpenShop)
            SetStateFinish(t_State,true);
    }

    public void Added()
    {
        if(t_State == Tutorial_State.AddingPrice)
            SetStateFinish(t_State,true);
    }

    public void Bought()
    {
        if(t_State == Tutorial_State.BuyingStuff)
            SetStateFinish(t_State,true);
    }

    public void Closed()
    {
        if(t_State == Tutorial_State.BoughtStuff)
            SetStateFinish(t_State,true);
    }

    public void OpenedGMC()
    {
        if(t_State == Tutorial_State.CheckBalance)
            SetStateFinish(t_State,true);
    }

    public void DailyCheck()
    {
        if(t_State == Tutorial_State.CheckDaily)
            SetStateFinish(t_State,true);
    }
}