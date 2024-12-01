using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance = null;
    [SerializeField] private List<GameObject> questPanels;
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


    //클릭된 퀘스트 최초 실행
    public void OnQuestPanelClicked(GameObject questObject)
    {
        Quest quest = questObject.GetComponent<Quest>();
        if (quest != null && quest.questStatus == QuestStatus.NotStarted)
        {//퀘스트가 존재하고 처음 시작했을때만 한번 실행
            Debug.Log(quest.QuestName() + " 최초실행");
            quest.StartQuest();
        }
    }
}
