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
    private const float dialogueDelay = 1.5f;
    private int waterClicked = 0;
    private QuestManager questManager;
    [SerializeField] Sprite[] fill70;
    [SerializeField] Sprite[] fill40;
    [SerializeField] Sprite[] fill0;
    private bool firstWaterTouchFlag = true;
    private string todoKey = "Rsub1";

    public override void Start()
    {
        questName = "CafeNamu_SubQuest";
    }
    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        inventoryManager = InventoryManager.InvenManager_Instance;
        questManager = QuestManager.QuestManager_instance;
        textManager = TextManager.TextManager_Instance;
        dialogueManager = DialogueManager.DialoguManager_Instance;
        locationAndTodoList = LocationAndTodoList.LocationAndTodoList_Instance;
        GetQuestDialogue("인게임 대사", "카페나무", "Rsub1");

        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            questManager.SaveQuestStatus();
            textManager.PopUpText(dialogue[0]);//시작 대사
        }
        else if (questStatus == QuestStatus.Completed)
        {
            waterClicked = 3;
            this.GetComponent<Image>().sprite = fill0[2];
        }
        inventoryManager.SetItemsOnInven(slotList);

        //todo생성
        locationAndTodoList.SetTodo(todoKey, dialogueManager.GetQuestDialogue("퀘스트 대사", "카페나무", "Rsub1"));
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + " clear");
        questStatus= QuestStatus.Completed;
        questManager.SaveQuestStatus();
        locationAndTodoList.DeleteTodo(todoKey);
    }

    //public override void Clicked()
    //{
    //    //Debug.Log("clicked");
    //}

    public void OnWaterClicked()
    {//물이 클릭되었을때
        if (InventoryManager.InvenManager_Instance.GetSelectedItemName() == "HandyLadle")
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
                    Invoke("Delay", 3f);
                    break;
                default:
                    break;
            }
        }
        else if (firstWaterTouchFlag && waterClicked == 0)
        {//맨처음 클릭
            textManager.PopUpText(dialogue[1]);
            firstWaterTouchFlag = false;
            StartCoroutine(DelayDialouge(dialogue[2]));
        }
    }
    IEnumerator ChangeImage(Sprite[] sprite)
    {
        water.GetComponent<Image>().raycastTarget = false;
        foreach (var frame in sprite)
        {
            yield return new WaitForSeconds(1f);
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
        OnQuestCompleted();
    }
    public override void ifQuestBtnClicked()
    {
        base.ifQuestBtnClicked();
    }
}
