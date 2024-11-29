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
    protected string questName; //����Ʈ �̸�
    protected int questNumber; //����Ʈ ������ ���� �ѹ���
    public QuestStatus questStatus = QuestStatus.NotStarted; //��� ����Ʈ�� ���� 
    protected List<Item> requiredItems; //����Ʈ Ŭ����� �ʿ��� �������� �ִٸ� ���
    public Quest(string questName, int questNumber, List<Item> requiredItems)
    {//����Ʈ ������
        this.questName = questName;
        this.requiredItems = requiredItems;
        questStatus = QuestStatus.NotStarted;
    }
    
    public virtual void StartQuest()
    {
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log($"{questName} ����Ʈ ����");
            OnQuestStarted(); //���� ����Ʈ ����
        }
    }
    protected abstract void OnQuestStarted();
    public virtual void InitializeQuest()
    {
        if (questStatus == QuestStatus.InProgress)
        {
            questStatus = QuestStatus.NotStarted;
            Debug.Log($"{questName} ����Ʈ �ʱ�ȭ");
        }
    }

    public virtual void CheckCompletion()
    {//����Ʈ Ŭ���� ����, �ʿ�� overriding ����
        if (questStatus == QuestStatus.InProgress)
        {
            // �ʿ��� �����۵��� ��� �ִ��� Ȯ��
            //bool allItemsPresent = requiredItems.TrueForAll(item => playerItems.Contains(item));
            if (true)
            {
                questStatus = QuestStatus.Completed;
                OnQuestCompleted(); //���� ����Ʈ Ŭ����� ����
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
{//����Ʈ ����
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