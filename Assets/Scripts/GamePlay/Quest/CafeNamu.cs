using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeNamu : Quest
{//퀘스트 예시
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
    }

    //public override void Clicked()
    //{
    //    //Debug.Log("clicked");
    //}

    public void OnClose()
    {
        GameManager.GameManager_Instance.isUiOpened = false;
    }

    public void OnWaterClicked()
    {//물이 클릭되었을때
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
