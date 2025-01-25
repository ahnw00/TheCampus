using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            //시작 대사 출력
            PrintDialogue(18);
        }
        inventoryManager.SetItemsOnInven(slotList);
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + " clear");
        PlayerPrefs.SetInt("Piece3", 1);
        PlayerPrefs.Save();
        questStatus= QuestStatus.Completed;
        questManager.SaveQuestStatus();
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
            }
        }
        else if (firstWaterTouchFlag && waterClicked == 0)
        {//맨처음 클릭
            PrintDialogue(19);
            firstWaterTouchFlag = false;
            StartCoroutine(DelayDialouge(20));
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
    IEnumerator DelayDialouge(int index)
    {
        yield return new WaitForSeconds(dialogueDelay);
        PrintDialogue(index);
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
