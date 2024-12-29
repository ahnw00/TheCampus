using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeNamu : Quest
{//����Ʈ ����
    [SerializeField] private GameObject water;
    private ItemClass item;
    private int waterClicked = 0;
    private QuestManager questManager;

    public override void Start()
    {
        inventoryManager = InventoryManager.InvenManager_Instance;
        questManager = QuestManager.QuestManager_instance;
        questName = "CafeNamu_SubQuest";
        requiredItems.Add("HandyLadle");
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
            }
            else
            {
                Debug.Log("water deleted");
                water.gameObject.SetActive(false);
                OnQuestCompleted();
            }
        }
    }

    public override void ifQuestBtnClicked() { }
}
