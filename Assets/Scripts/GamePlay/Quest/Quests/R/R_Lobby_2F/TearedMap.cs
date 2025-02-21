using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearedMap : Quest
{
    //private QuestManager questManager;
    [SerializeField] private List<TearedPiece> pieceList = new List<TearedPiece>();
    [SerializeField] private GameObject moveToH;
    //private string todoKey = "Rmain";

    private void Awake()
    {
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
        //questManager = QuestManager.QuestManager_instance;
        inventoryManager = InventoryManager.InvenManager_Instance;
        //inventoryManager.SetItemsOnInven(slotList);
        //GetQuestDialogue("인게임 대사", "R동 전체", "Rmain");
        questName = "TearedMap_MainQuest";
        InitializeQuest();
    }

    public override void StartQuest()
    {//Äù½ºÆ®°¡ ½ÃÀÛÇÒ¶§ ½ÇÇà
        InitializeQuest();
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            questManager.SaveQuestStatus();
            Debug.Log(questName + " ½ÃÀÛ");
        }
    }

    public override void QuitQuest()
    {
        base.QuitQuest();
    }

    public override void ifQuestBtnClicked()
    {
        InitializeQuest();
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
                OnQuestCompleted(); //°³º° Äù½ºÆ® Å¬¸®¾î½Ã ½ÇÇà
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
