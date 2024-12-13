using UnityEngine;

public class ExhibitionHall : Quest
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        inventoryManager = InventoryManager.InvenManager_Instance;

        questName = "ExhibitionHall_SubQuest";
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
        Debug.Log($"item get");
    }

    public void OnPaintClicked()
    {
        //if(inventoryManager.LastClickedItem == )
    }
}
