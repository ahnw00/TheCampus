using System;
using System.Collections.Generic;
using UnityEngine;
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
    protected int questNumber; //퀘스트 구별을 위한 넘버링
    public QuestStatus questStatus = QuestStatus.NotStarted; //모든 퀘스트는 시작 
    protected List<Item> requiredItems; //퀘스트 클리어시 필요한 아이템이 있다면 사용
    public Quest(string questName, int questNumber, List<Item> requiredItems)
    {//퀘스트 생성자
        this.questName = questName;
        this.requiredItems = requiredItems;
        questStatus = QuestStatus.NotStarted;
    }
    
    public virtual void StartQuest()
    {
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log($"{questName} 퀘스트 시작");
            OnQuestStarted(); //개별 퀘스트 내용
        }
    }
    protected abstract void OnQuestStarted();
    public virtual void InitializeQuest()
    {
        if (questStatus == QuestStatus.InProgress)
        {
            questStatus = QuestStatus.NotStarted;
            Debug.Log($"{questName} 퀘스트 초기화");
        }
    }

    public virtual void CheckCompletion()
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

    protected abstract void OnQuestCompleted();
    public virtual int QuestNumber()
    {
        return questNumber;
    }
}

public class CafeNamu : Quest
{//퀘스트 예시
    public CafeNamu() : base("CafeNamu_SubQuest", 0, null) { }
    protected override void OnQuestStarted()
    {
        Debug.Log($"{questName} start");
    }

    protected override void OnQuestCompleted()
    {
        Debug.Log($"{questName} clear");
    }
}