using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeNamu : Quest
{//퀘스트 예시
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
        Debug.Log($"item picture 3 get");
        questStatus= QuestStatus.Completed;
        questManager.SaveQuestStatus();
    }

    //public override void Clicked()
    //{
    //    //Debug.Log("clicked");
    //}

    public void OnWaterClicked()
    {//물이 클릭되었을때
        if (InventoryManager.InvenManager_Instance.GetSelectedItemName() == requiredItems[0])
        {
            //lastClickedItem이 HandyLadle일때만
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
