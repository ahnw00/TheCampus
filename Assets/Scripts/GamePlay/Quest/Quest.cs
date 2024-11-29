using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public enum QuestStatus
{
    NotStarted,
    InProgress,
    Completed
}

public abstract class Quest
{
    protected string questName; //����Ʈ �̸�
    protected int questNumber; //����Ʈ ������ ���� �ѹ���
    public QuestStatus questStatus = QuestStatus.NotStarted; //��� ����Ʈ�� ���� 
    protected List<Item> requiredItems; //����Ʈ Ŭ����� �ʿ��� �������� �ִٸ� ���
    
    public Quest(string questName, int questNumber, List<Item> requiredItems)
    {//����Ʈ ������
        this.questName = questName;
        this.questNumber = questNumber;
        this.requiredItems = requiredItems;
        questStatus = QuestStatus.NotStarted;
        InitializeQuest();//����Ʈ ������ �ʿ��� �͵� �ʱ�ȭ
    }
    public virtual void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(this.questName + "start");
            OnQuestStarted(); //���� ����Ʈ ����
        }
    }
    protected abstract void OnQuestStarted();
    public virtual void InitializeQuest() { }

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

