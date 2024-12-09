using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeNamu : Quest
{//����Ʈ ����
    [SerializeField] private GameObject water;
    private int waterClicked = 0;

    public override void Start()
    {
        inventoryManager = InventoryManager.InvenManager_Instance;

        questName = "CafeNamu_SubQuest";
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
        Debug.Log($"item picture 3 get");
    }

    //public override void Clicked()
    //{
    //    //Debug.Log("clicked");
    //}

    public void OnWaterClicked()
    {//���� Ŭ���Ǿ�����
        //if(inventoryManager.LastClickedItem)
        if(waterClicked < 2)
        {
            waterClicked++;
            Debug.Log(waterClicked);
        }
        else
        {
            Debug.Log("water deleted");
            water.gameObject.SetActive(false);
            OnQuestCompleted();
        }
    }
}
