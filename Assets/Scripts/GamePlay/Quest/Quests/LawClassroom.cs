using System.Collections.Generic;
using UnityEngine;

public class LawClassroom : Quest
{
    [SerializeField] private List<ItemSlot> slotList = new List<ItemSlot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        inventoryManager = InventoryManager.InvenManager_Instance;
        inventoryManager.SetItemsOnInven(slotList);

        questName = "LawClassroom_SubQuest";
        questNumber = 0;
        requiredItems = null;
    }
    public override void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(questName + " ����");
        }
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
