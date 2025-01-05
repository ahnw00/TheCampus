using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public enum QuestStatus
{
    NotStarted,
    InProgress,
    Completed
}

public abstract class Quest : MonoBehaviour
{
    protected string questName; //퀘스트 이름
    public QuestStatus questStatus = QuestStatus.NotStarted; //모든 퀘스트는 시작 
    protected List<string> requiredItems = new List<string>(); //퀘스트 클리어시 필요한 아이템이 있다면 사용
    protected InventoryManager inventoryManager;
    [SerializeField] protected List<ItemSlot> slotList;
    [SerializeField] public GameObject questInven;

    /*
    public Quest(string questName, int questNumber, List<Item> requiredItems)
    {//퀘스트 생성자
        this.questName = questName;
        this.questNumber = questNumber;
        this.requiredItems = requiredItems;
        questStatus = QuestStatus.NotStarted;
        InitializeQuest();//퀘스트 생성시 필요한 것들 초기화
    }*/
    public abstract void Start();
    public virtual void StartQuest()
    {//퀘스트가 시작할때 실행
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(this.questName + "start");
        }
    }

    public virtual void QuitQuest()
    {
        if(questStatus != QuestStatus.Completed)
        {
            questStatus = QuestStatus.NotStarted ;
        }
    }

    protected virtual void CheckCompletion()
    {//퀘스트 클리어 조건, 필요시 overriding 가능
        if (questStatus == QuestStatus.InProgress)
        {
            // 필요한 아이템들이 모두 있는지 확인
            //bool allItemsPresent = requiredItems.TrueForAll(item => playerItems.Contains(item));
            if (true)
            {
                questStatus = QuestStatus.Completed;
                OnQuestCompleted(); //개별 퀘스트 클리어시 실행
            }
            else
            {
                Debug.Log($"{questName} not clear");
            }
        }
    }

    public virtual void ifQuestBtnClicked()
    {//quest버튼이 눌렸을 때마다 실행되는 함수. 여기서는 인벤을 불러온다.
        questInven.SetActive(true);
        foreach (var slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }
        inventoryManager.SetItemsOnInven(slotList);
    }
    protected abstract void OnQuestCompleted();
    public virtual string QuestName()
    {
        return questName;
    }
}

