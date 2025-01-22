using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeNamu : Quest
{//����Ʈ ����
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject Piece3;
    private const float delaySecond = 1.5f;
    private ItemClass item;
    private int waterClicked = 0;
    private QuestManager questManager;
    [SerializeField] FadeEffect fadeEffect;
    [SerializeField] Sprite fill70;
    [SerializeField] Sprite fill40;
    [SerializeField] Sprite fill0;

    public override void Start()
    {
        questName = "CafeNamu_SubQuest";
    }
    public override void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
        inventoryManager = InventoryManager.InvenManager_Instance;
        questManager = QuestManager.QuestManager_instance;
        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(questName + " ����");
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
    {//���� Ŭ���Ǿ�����
        if (InventoryManager.InvenManager_Instance.GetSelectedItemName() == "HandyLadle")
        {
            //lastClickedItem�� HandyLadle�϶���
            switch (waterClicked)
            {
                case 0:
                    waterClicked++;
                    StartCoroutine(ChangeImage(fill70));
                    fadeEffect.FadeOutIn(delaySecond, delaySecond);
                    break;
                case 1:
                    waterClicked++;
                    StartCoroutine(ChangeImage(fill40));
                    fadeEffect.FadeOutIn(delaySecond, delaySecond);
                    break;
                case 2:
                    waterClicked++;
                    StartCoroutine(ChangeImage(fill0));
                    fadeEffect.FadeOutIn(delaySecond, delaySecond);
                    water.SetActive(false);
                    Invoke("Delay", delaySecond);
                    break;
            }
        }
    }
    IEnumerator ChangeImage(Sprite sprite)
    {
        yield return new WaitForSeconds(delaySecond);
        this.GetComponent<Image>().sprite = sprite;
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
