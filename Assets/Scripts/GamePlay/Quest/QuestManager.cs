using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance = null;
    [SerializeField] private List<GameObject> questPanels;
    [SerializeField] private GameObject inventoryBtn;
    private DataManager dataManager;
    private SaveDataClass saveData;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("QuestManager duplicate error, destroying duplicate instance.");
            Destroy(this.gameObject);
        }

        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
        InitializeQuestStatus();
        SaveQuestStatus();
    }

    public static QuestManager QuestManager_instance
    {
        get
        {
            if (null == instance)
            {
                Debug.Log("QuestManager is null");
                return null;
            }
            return instance;
        }
    }

    private void InitializeQuestStatus()
    {
        //questStatus 초기화
        if (saveData.questList == null)
        {//처음 실행시 NotStarted로 초기화
            string questState = "NotStarted";
            foreach (var quest in questPanels)
            {
                saveData.questList.Add(questState);
            }
        }
        else
        {//데이터가 있는경우
            int i = 0;
            foreach (var state in saveData.questList)
            {
                if (Enum.TryParse(state, out QuestStatus questStatus))
                {
                    questPanels[i].GetComponent<Quest>().questStatus = questStatus;
                    i++;
                }
                else
                {
                    Debug.LogError("quest " + i + " Not Exist QuestStatus");
                    i++;
                }
            }
        }
    }

    //Å¬¸¯µÈ Äù½ºÆ® ÃÖÃÊ ½ÇÇà
    public void OnQuestBtnClicked(GameObject questObject)
    {
        Quest quest = questObject.GetComponent<Quest>();
        if (quest != null && quest.questStatus == QuestStatus.NotStarted)
        {//퀘스트 처음 시작시 실행
            Debug.Log(quest.QuestName() + " Start");
            quest.StartQuest();
        }
        else if (quest != null && quest.questStatus == QuestStatus.InProgress)
        {//퀘스트 버튼이 클릭될 때마다 실행시 작성
            quest.ifQuestBtnClicked();
        }
    }

    public void SaveQuestStatus()
    {//현재 퀘스트 상태 저장
        saveData.questList.Clear();
        foreach(var quest in questPanels)
        {
            saveData.questList.Add(quest.GetComponent<Quest>().questStatus.ToString());
        }
        dataManager.Save();
    }
}
