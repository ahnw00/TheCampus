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


    //Å¬¸¯µÈ Äù½ºÆ® ÃÖÃÊ ½ÇÇà
    public void OnQuestPanelClicked(GameObject questObject)
    {
        Quest quest = questObject.GetComponent<Quest>();
        if (quest != null && quest.questStatus == QuestStatus.NotStarted)
        {//Äù½ºÆ®°¡ Á¸ÀçÇÏ°í Ã³À½ ½ÃÀÛÇßÀ»¶§¸¸ ÇÑ¹ø ½ÇÇà
            Debug.Log(quest.QuestName() + " ÃÖÃÊ½ÇÇà");
            quest.StartQuest();
        }
    }
}
