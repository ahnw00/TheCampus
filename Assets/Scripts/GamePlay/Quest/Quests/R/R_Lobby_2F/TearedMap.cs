using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearedMap : Quest
{
    [SerializeField] private List<TearedPiece> pieceList = new List<TearedPiece>();
    [SerializeField] private GameObject moveToH;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        inventoryManager = InventoryManager.InvenManager_Instance;
        //inventoryManager.SetItemsOnInven(slotList);

        questName = "TearedMap_MainQuest";
        requiredItems = null;

        if(!PlayerPrefs.HasKey("Piece5"))
            PlayerPrefs.SetInt("Piece5", 1);
        if (!PlayerPrefs.HasKey("Piece6"))
            PlayerPrefs.SetInt("Piece6", 1);
        PlayerPrefs.Save();
    }

    public override void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(questName + " ����");
        }
    }

    public override void QuitQuest()
    {
        base.QuitQuest();
    }

    protected override bool CheckCompletion()
    {
        if (questStatus == QuestStatus.InProgress)
        {
            bool flag = true;
            foreach (var piece in pieceList)
            {
                if (!piece.foundCorrectPos)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                questStatus = QuestStatus.Completed;
                OnQuestCompleted(); //���� ����Ʈ Ŭ����� ����
                return true;
            }
            else
            {
                Debug.Log($"{questName} not clear");
                return false;
            }
        }
        return false;
    }

    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
        moveToH.SetActive(true);
    }
}
