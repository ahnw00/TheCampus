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
    public Action OnQuestCompleted; // ����Ʈ �Ϸ� �� ����� ����

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
            Debug.Log($"{QuestName} ����Ʈ ����");
        }
    }

    public void CheckCompletion(List<Item> playerItems)
    {
        if (Status == QuestStatus.InProgress)
        {
            // �ʿ��� �����۵��� ��� �ִ��� Ȯ��
            bool allItemsPresent = RequiredItems.TrueForAll(item => playerItems.Contains(item));
            if (allItemsPresent)
            {
                Status = QuestStatus.Completed;
                Debug.Log($"{QuestName} ����Ʈ �Ϸ�");
                OnQuestCompleted?.Invoke(); // ����Ʈ �Ϸ� �̺�Ʈ ����
            }
        }
    }

    /* ����Ʈ �ۼ� ����
     * findKeyQuest = new Quest(
            "Find the Key",
            requiredItems,
            () =>
            {
                Debug.Log("����ȹ��, ����Ʈ Ŭ����");
            }
        );
     */
}
