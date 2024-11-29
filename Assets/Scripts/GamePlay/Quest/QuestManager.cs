using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance = null;
    [SerializeField] Button[] buttons;
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
    {//���⼭ ��� ����Ʈ�� �ʱ�ȭ�ϰ� ����Ѵ�
        CafeNamu cafeNamu = new CafeNamu();
        buttonQuestMap.Add(buttons[cafeNamu.QuestNumber()], cafeNamu);
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
            quest.StartQuest();
        }
        else
        {
            Debug.LogError("null quest mapped");
        }
    }
}
