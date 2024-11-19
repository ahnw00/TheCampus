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

public class Quest
{
    public string QuestName;
    public QuestStatus Status;
    public List<Item> RequiredItems;
    public Action OnQuestCompleted; // 퀘스트 완료 시 실행될 동작

    public Quest(string questName, List<Item> requiredItems, Action onQuestCompleted)
    {
        QuestName = questName;
        RequiredItems = requiredItems;
        Status = QuestStatus.NotStarted;
        OnQuestCompleted = onQuestCompleted;
    }

    public void StartQuest()
    {
        if (Status == QuestStatus.NotStarted)
        {
            Status = QuestStatus.InProgress;
            Debug.Log($"{QuestName} 퀘스트 시작");
        }
    }

    public void CheckCompletion(List<Item> playerItems)
    {
        if (Status == QuestStatus.InProgress)
        {
            // 필요한 아이템들이 모두 있는지 확인
            bool allItemsPresent = RequiredItems.TrueForAll(item => playerItems.Contains(item));
            if (allItemsPresent)
            {
                Status = QuestStatus.Completed;
                Debug.Log($"{QuestName} 퀘스트 완료");
                OnQuestCompleted?.Invoke(); // 퀘스트 완료 이벤트 실행
            }
        }
    }

    /* 퀘스트 작성 예시
     * findKeyQuest = new Quest(
            "Find the Key",
            requiredItems,
            () =>
            {
                Debug.Log("열쇠획득, 퀘스트 클리어");
            }
        );
     */
}
