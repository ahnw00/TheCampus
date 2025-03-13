using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CafeNamu : Quest
{//퀘스트 예시
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject Piece3;
    [SerializeField] AudioSource waterSFX;
    private const float dialogueDelay = 2f;
    private int waterClicked = 0;
    [SerializeField] Sprite[] fill70;
    [SerializeField] Sprite[] fill40;
    [SerializeField] Sprite[] fill0;
    private bool firstWaterTouchFlag = true;
    private string todoKey = "Rsub1";
    [SerializeField] private AudioClip completeClip;

    public override void Start()
    {
        questName = "CafeNamu_SubQuest";
        soundManager = SoundManager.Instance;
    }
    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        InitializeQuest();
        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            questManager.SaveQuestStatus();
            textManager.PopUpText(dialogue[0]);//시작 대사
            Record.Record_Instance.AddText(TextType.Speaker, dialogue[0], "나");

            //todo생성
            locationAndTodoList.SetTodo(todoKey, dialogueManager.GetQuestDialogue("퀘스트 대사", todoKey));
        }
        else if (questStatus == QuestStatus.Completed)
        {
            waterClicked = 3;
            this.GetComponent<Image>().sprite = fill0[2];
        }
        base.ifQuestBtnClicked();
    }
    protected override void OnQuestCompleted()
    {
        soundManager.ChangeSfxClip(completeClip);
        Debug.Log(questName + " clear");
        questStatus= QuestStatus.Completed;
        questManager.SaveQuestStatus();
        locationAndTodoList.DeleteTodo(todoKey);
        PrintMainQuestDialogue();
    }

    //public override void Clicked()
    //{
    //    //Debug.Log("clicked");
    //}

    public void OnWaterClicked()
    {//물이 클릭되었을때
        if (inventoryManager.GetSelectedItemName() == "HandyLadle" || inventoryManager.GetSelectedItemName() == "StickyHandyLadle")
        {
            //lastClickedItem이 HandyLadle일때만
            switch (waterClicked)
            {
                case 0:
                    waterClicked++;
                    StartCoroutine(ChangeImage(fill70));
                    //fadeEffect.FadeOutIn(delaySecond, delaySecond);
                    break;
                case 1:
                    waterClicked++;
                    StartCoroutine(ChangeImage(fill40));
                    //fadeEffect.FadeOutIn(delaySecond, delaySecond);
                    break;
                case 2:
                    waterClicked++;
                    StartCoroutine(ChangeImage(fill0));
                    //fadeEffect.FadeOutIn(delaySecond, delaySecond);
                    water.SetActive(false);
                    //waterSFX.Play();
                    Invoke("Delay", 1.5f);
                    inventoryManager.ChangeHandyLadle();
                    break;
                default:
                    break;
            }
        }
        else if (firstWaterTouchFlag && waterClicked == 0)
        {//맨처음 클릭
            Record.Record_Instance.AddText(TextType.Speaker, dialogue[1], "나");
            textManager.PopUpText(dialogue[1]);
            firstWaterTouchFlag = false;
            StartCoroutine(DelayDialouge(dialogue[2]));
            Record.Record_Instance.AddText(TextType.Speaker, dialogue[2], "나");
        }
        else if (inventoryManager.GetSelectedItemName() == "TearedPaperCup" && waterClicked < 3)
        {
            textManager.PopUpText("찢어져 있어 물을 퍼낼 수 없을 것 같다");
        }
        else
        {
            if (inventoryManager.GetSelectedItemName() != "null" && waterClicked < 3)
            {
                textManager.PopUpText("이 물건은 관련있지 않은 것 같다");
            }
        }
    }
    IEnumerator ChangeImage(Sprite[] sprite)
    {
        water.GetComponent<Image>().raycastTarget = false;
        foreach (var frame in sprite)
        {
            yield return new WaitForSeconds(0.5f);
            this.GetComponent<Image>().sprite = frame;
        }
        water.GetComponent<Image>().raycastTarget = true;
    }
    IEnumerator DelayDialouge(string dialogue)
    {
        yield return new WaitForSeconds(dialogueDelay);
        textManager.PopUpText(dialogue);
    }
    void Delay()
    {
        Piece3.SetActive(true);
    }

    public void OnPieceClicked()
    {
        Piece3.SetActive(false);
        PlayerPrefs.SetInt("Piece3", 1);
        PlayerPrefs.Save();
        OnQuestCompleted();
    }
    public override void ifQuestBtnClicked()
    {
        InitializeQuest();
        base.ifQuestBtnClicked();
    }

    protected override void InitializeQuest()
    {
        base.InitializeQuest();
        GetQuestDialogue("인게임 대사", "카페나무", "Rsub1");
    }
}
