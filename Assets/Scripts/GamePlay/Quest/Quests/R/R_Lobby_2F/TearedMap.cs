using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearedMap : Quest
{
    private QuestManager questManager;
    [SerializeField] private List<TearedPiece> pieceList = new List<TearedPiece>();
    [SerializeField] private GameObject moveToH;

    private void Awake()
    {
        //if (!PlayerPrefs.HasKey("Piece1"))
        //{
        //    Debug.Log("5 obtained");
        //    PlayerPrefs.SetInt("Piece1", 1);
        //}
        //if (!PlayerPrefs.HasKey("Piece2"))
        //{
        //    Debug.Log("5 obtained");
        //    PlayerPrefs.SetInt("Piece2", 1);
        //}
        //if (!PlayerPrefs.HasKey("Piece3"))
        //{
        //    Debug.Log("5 obtained");
        //    PlayerPrefs.SetInt("Piece3", 1);
        //}
        //if (!PlayerPrefs.HasKey("Piece4"))
        //{
        //    Debug.Log("5 obtained");
        //    PlayerPrefs.SetInt("Piece4", 1);
        //}
        if (!PlayerPrefs.HasKey("Piece5"))
        {
            Debug.Log("5 obtained");
            PlayerPrefs.SetInt("Piece5", 1);
        }

        if (!PlayerPrefs.HasKey("Piece6"))
        {
            Debug.Log("6 obtained");
            PlayerPrefs.SetInt("Piece6", 1);
        }
        PlayerPrefs.Save();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        questManager = QuestManager.QuestManager_instance;
        inventoryManager = InventoryManager.InvenManager_Instance;
        //inventoryManager.SetItemsOnInven(slotList);

        questName = "TearedMap_MainQuest";
    }

    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(questName + " 시작");
        }
    }

    public override void QuitQuest()
    {
        base.QuitQuest();
    }

    public override void ifQuestBtnClicked()
    {
        foreach (var piece in pieceList)
        {
            if(PlayerPrefs.HasKey(piece.gameObject.name))
                piece.gameObject.SetActive(true);
        }
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
                OnQuestCompleted(); //개별 퀘스트 클리어시 실행
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

    public bool Check()
    {
        return CheckCompletion();
    }

    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
        moveToH.SetActive(true);
        questManager.SaveQuestStatus();
    }
}
