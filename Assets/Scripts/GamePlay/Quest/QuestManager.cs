using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance = null;
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] questPanels;
    private Dictionary<Button, Quest> buttonQuestMap = new Dictionary<Button, Quest>(); // ��ư-����Ʈ ����


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
    {//���⼭ ��� ����Ʈ�� �ʱ�ȭ�ϰ� ����Ʈ �Ŵ����� ����Ѵ�
        CafeNamu cafeNamu = new CafeNamu();
        buttonQuestMap.Add(buttons[cafeNamu.QuestNumber()], cafeNamu); //buttonQuestMap[0, cafeNamu]
    }
    private void InitializeButtons()
    {
        foreach (var button in buttons)
        {
            // �� ��ư�� Ŭ���� �� OnButtonClicked����
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    //Ŭ���� ��ư�� �̺�Ʈ ����
    private void OnButtonClicked(Button clickedButton)
    {
        if (buttonQuestMap.ContainsKey(clickedButton))
        {//��ư-����Ʈ ������ �ùٸ��ٸ�
            Quest quest = buttonQuestMap[clickedButton];
            if (quest != null && quest.questStatus == QuestStatus.NotStarted) 
            {//����Ʈ�� �����ϰ� ó�� ������������ �ѹ� ����
                Debug.Log("����Ʈó������");
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
