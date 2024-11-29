using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance = null;
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] questPanels;
    private Dictionary<Button, Quest> buttonQuestMap = new Dictionary<Button, Quest>(); // 버튼-퀘스트 매핑


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
        InitializeQuests();
        InitializeButtons();
    }

    public static QuestManager QuestManager_instance
    {
        get
        {
            if (null == instance)
            {
                Debug.Log("MapManager is null");
                return null;
            }
            return instance;
        }
    }
    private void InitializeQuests()
    {//여기서 모든 퀘스트를 초기화하고 퀘스트 매니저에 등록한다
        CafeNamu cafeNamu = new CafeNamu();
        buttonQuestMap.Add(buttons[cafeNamu.QuestNumber()], cafeNamu); //buttonQuestMap[0, cafeNamu]
    }
    private void InitializeButtons()
    {
        foreach (var button in buttons)
        {
            // 각 버튼이 클릭될 시 OnButtonClicked실행
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    //클릭된 버튼의 이벤트 실행
    private void OnButtonClicked(Button clickedButton)
    {
        if (buttonQuestMap.ContainsKey(clickedButton))
        {//버튼-퀘스트 매핑이 올바르다면
            Quest quest = buttonQuestMap[clickedButton];
            if (quest != null && quest.questStatus == QuestStatus.NotStarted) 
            {//퀘스트가 존재하고 처음 실행했을때만 한번 실행
                Debug.Log("퀘스트처음실행");
                quest.StartQuest();
            }
        }
        else
        {
            Debug.LogError("null quest mapped");
        }
    }

    public Button GetQuestPanelButton(int num)
    {
        return buttons[num];
    }
    public GameObject GetQuestPanelObject(int num)
    {
        return questPanels[num];
    }
}
