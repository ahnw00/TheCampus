using UnityEngine;

public class LawClassroom : Quest
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        inventoryManager = InventoryManager.InvenManager_Instance;

        questName = "LawClassroom_SubQuest";
        questNumber = 0;
        requiredItems = null;
    }
    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(questName + " 시작");
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
