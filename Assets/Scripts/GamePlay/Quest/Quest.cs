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
    protected string questName; //����Ʈ �̸�
    public QuestStatus questStatus = QuestStatus.NotStarted; //��� ����Ʈ�� ���� 
    protected List<string> requiredItems = new List<string>(); //����Ʈ Ŭ����� �ʿ��� �������� �ִٸ� ���
    protected InventoryManager inventoryManager;
    [SerializeField] protected List<ItemSlot> slotList;
    [SerializeField] public GameObject questInven;

    /*
    public Quest(string questName, int questNumber, List<Item> requiredItems)
    {//����Ʈ ������
        this.questName = questName;
        this.questNumber = questNumber;
        this.requiredItems = requiredItems;
        questStatus = QuestStatus.NotStarted;
        InitializeQuest();//����Ʈ ������ �ʿ��� �͵� �ʱ�ȭ
    }*/
    public abstract void Start();
    public virtual void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
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

    public virtual void ifQuestBtnClicked()
    {//quest��ư�� ������ ������ ����Ǵ� �Լ�. ���⼭�� �κ��� �ҷ��´�.
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

