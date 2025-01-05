using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeNamu : Quest
{//����Ʈ ����
    [SerializeField] private GameObject water;
    private ItemClass item;
    private int waterClicked = 0;
    private QuestManager questManager;
    [SerializeField] FadeEffect fadeEffect;

    public override void Start()
    {
        questName = "CafeNamu_SubQuest";
        requiredItems.Add("HandyLadle");
    }
    public override void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
        inventoryManager = InventoryManager.InvenManager_Instance;
        questManager = QuestManager.QuestManager_instance;
        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(this.questName + " ����");
        }
        inventoryManager.SetItemsOnInven(slotList);
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(this.questName + " clear");
        PlayerPrefs.SetInt("Piece3", 1);
        PlayerPrefs.Save();
        Debug.Log($"item picture 3 get");
        questStatus= QuestStatus.Completed;
        questManager.SaveQuestStatus();
    }

    //public override void Clicked()
    //{
    //    //Debug.Log("clicked");
    //}

    public void OnWaterClicked()
    {//���� Ŭ���Ǿ�����
        if (InventoryManager.InvenManager_Instance.GetSelectedItemName() == requiredItems[0])
        {
            //lastClickedItem�� HandyLadle�϶���
            if (waterClicked < 2)
            {
                waterClicked++;
                Debug.Log(waterClicked);
                fadeEffect.FadeOutIn(1.5f, 1.5f);
            }
            else
            {
                Debug.Log("water deleted");
                water.gameObject.SetActive(false);
                OnQuestCompleted();
            }
        }
    }
    public override void ifQuestBtnClicked()
    {
        base.ifQuestBtnClicked();
    }
}
