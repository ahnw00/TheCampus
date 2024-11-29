using UnityEngine;
using UnityEngine.UI;

public class CafeNamu : Quest
{//퀘스트 예시
    [SerializeField] GameObject cafeNamu_subQuest_Panel;
    private Button questOpenButton; //퀘스트 패널 열기
    private Button quitButton; // 퀘스트 패널 닫기
    private Button waterButton; // 물 퍼내기
    private int waterClicked = 3;
    public CafeNamu() : base("CafeNamu_SubQuest", 0, null) { }
    /*
    void Start()
    {
        questName = "CafeNamu_SubQuest";
        questNumber= 0;
        requiredItems = null;
    }*/
    
    public override void InitializeQuest()
    {//필요한 것들 초기화
        cafeNamu_subQuest_Panel = QuestManager.QuestManager_instance.GetQuestPanelObject(this.questNumber);
        waterButton = cafeNamu_subQuest_Panel.transform.GetChild(0).GetComponent<Button>();
        quitButton = cafeNamu_subQuest_Panel.transform.GetChild(1).GetComponent<Button>();
        waterButton.onClick.AddListener(OnWaterClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        questOpenButton = QuestManager.QuestManager_instance.GetQuestPanelButton(this.questNumber);
        questOpenButton.onClick.AddListener(OnPanelClicked);
        //GameObject[] questPanels = GameObject.FindGameObjectsWithTag("Quest");
        /*
        if (questPanels != null)
        {
            foreach (GameObject obj in questPanels)
            {
                if (obj.name == "CafeNamu_SubQuest_Panel")
                {
                    cafeNamu_subQuest_Panel = obj;
                }
            }
        }
        else
        {
            Debug.Log("null quest panels");
        }
        */

    }
    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            cafeNamu_subQuest_Panel.SetActive(true);
            Debug.Log(questName + "퀘스트 시작");
            OnQuestStarted(); //개별 퀘스트 내용
        }
    }

    protected override void OnQuestStarted()
    {
        //cafeNamu_subQuest_Panel.SetActive(true);
    }

    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
        Debug.Log($"item picture 3 get");
    }
    private void OnQuitClicked()
    {//나가기 버튼
        cafeNamu_subQuest_Panel.SetActive(false);
    }
    private void OnWaterClicked()
    {//물이 클릭되었을때
        if(waterClicked > 0)
        {
            waterClicked--;
            Debug.Log(waterClicked);
        }
        else
        {
            Debug.Log("water deleted");
            waterButton.gameObject.SetActive(false);
            OnQuestCompleted();
        }
    }
    private void OnPanelClicked()
    {
        cafeNamu_subQuest_Panel.SetActive(true);
    }
}
