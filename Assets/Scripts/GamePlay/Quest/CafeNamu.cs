using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CafeNamu : Quest
{//����Ʈ ����
    [SerializeField] private GameObject cafeNamu_SubQuest;
    [SerializeField] private GameObject quit;
    [SerializeField] private GameObject water;
    private int waterClicked = 3;

    public override void Start()
    {
        questName = "CafeNamu_SubQuest";
        questNumber = 0;
        requiredItems = null;
        cafeNamu_SubQuest.SetActive(false);
        quit.SetActive(true);
        water.SetActive(true);
    }
    public override void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(questName + " ����");
        }
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
        Debug.Log($"item picture 3 get");
    }

    public override void Clicked()
    {
        //Debug.Log("clicked");
    }

    private void OnWaterClicked()
    {//���� Ŭ���Ǿ�����
        if(waterClicked > 0)
        {
            waterClicked--;
            Debug.Log(waterClicked);
        }
        else
        {
            Debug.Log("water deleted");
            water.gameObject.SetActive(false);
            OnQuestCompleted();
        }
    }
}
