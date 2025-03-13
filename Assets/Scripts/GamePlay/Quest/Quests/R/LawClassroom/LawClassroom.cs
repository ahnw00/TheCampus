using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawClassroom : Quest
{
    [SerializeField] public Scale leftScale, rightScale;
    [SerializeField] private RectTransform leftAnchor, rightAnchor;
    [SerializeField] private RectTransform topScale;
    private float leftAnchorY, rightAnchorY;
    private float target;
    private float startRot = 4f;
    public bool isCoroutineRunning = false;
    [SerializeField] private GameObject rustedSword;
    private string todoKey = "Rsub2";
    [SerializeField] private GameObject highlight;

    [SerializeField] private SpriteRenderer lawClassroomSR;
    [SerializeField] private Sprite completedSprite;

    [SerializeField] private AudioClip completeClip;
    [SerializeField] private AudioClip scaleClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {

        //inventoryManager = InventoryManager.InvenManager_Instance;
        //inventoryManager.SetItemsOnInven(slotList);

        questName = "LawClassroom_SubQuest";
        topScale.localRotation = Quaternion.Euler(0, 0, startRot);
        soundManager = SoundManager.Instance;
    }
    void Update()
    {
        //fix scales' rotation z
        leftScale.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        rightScale.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);

        float topZ = topScale.rotation.eulerAngles.z;
        leftAnchor.localRotation = Quaternion.Euler(0, 0, -topZ);
        rightAnchor.localRotation = Quaternion.Euler(0, 0, -topZ);
    }
    public override void StartQuest()
    {//Äù½ºÆ®°¡ ½ÃÀÛÇÒ¶§ ½ÇÇà
        InitializeQuest();
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            questManager.SaveQuestStatus();
            ifQuestBtnClicked();
            Debug.Log(questName + " ½ÃÀÛ");
            textManager.PopUpText(dialogue[0]);//시작 대사
                                               //todo생성
            locationAndTodoList.SetTodo(todoKey, dialogueManager.GetQuestDialogue("퀘스트 대사", todoKey));
        }
    }

    public override void QuitQuest()
    {
        foreach (Transform child in leftScale.transform)
        {
            if(child.childCount == 1)
                Destroy(child.GetChild(0).gameObject);
        }
        foreach (Transform child in rightScale.transform)
        {
            if (child.childCount == 1)
                Destroy(child.GetChild(0).gameObject);
        }

        topScale.rotation = Quaternion.Euler(0, 0, 4f);
    }

    protected override void OnQuestCompleted()
    {
        soundManager.ChangeSfxClip(completeClip);
        Debug.Log(questName + "clear");
        //inventoryManager.ObtainItem("RustedSword");
        rustedSword.SetActive(true);
        questStatus = QuestStatus.Completed;
        questManager.SaveQuestStatus();
        PlayerPrefs.SetInt("LawClassroom", 1);
        PlayerPrefs.Save();
        lawClassroomSR.sprite = completedSprite;
        PrintMainQuestDialogue();
        textManager.PopUpText(dialogue[1]);
        locationAndTodoList.DeleteTodo(todoKey);
    }

    protected override bool CheckCompletion()
    {
        if(questStatus == QuestStatus.InProgress)
        {
            if(target == 0)
            {
                questStatus = QuestStatus.Completed;
                return true;
            }
            return false;
        }
        return false;
    }

    public IEnumerator ScaleCoroutine()
    {
        soundManager.ChangeSfxClip(scaleClip);
        float angle = 4;
        float speed = 1.5f;
        target = 4 + (leftScale.sum - rightScale.sum) * angle;
        if (target < -16) target = -16;
        else if (target > 16) target = 16;

        float time = 0;
        Quaternion origin = topScale.rotation;
        float rotZ = origin.eulerAngles.z;
        Debug.Log(rotZ);
        if(rotZ > 90f) rotZ = rotZ - 360f;
        float gap = Mathf.Abs(rotZ - target);
        //Debug.Log(gap);

        isCoroutineRunning = true;
        while (time < gap / angle)
        {
            time += Time.deltaTime * speed;

            topScale.rotation = Quaternion.Slerp(origin, Quaternion.Euler(0f, 0f, target), time / gap * angle);
            yield return null;
        }
        isCoroutineRunning = false;

        if (CheckCompletion()) OnQuestCompleted();
    }
    public override void ifQuestBtnClicked()
    {//quest¹öÆ°ÀÌ ´­·ÈÀ» ¶§¸¶´Ù ½ÇÇàµÇ´Â ÇÔ¼ö. ¿©±â¼­´Â ÀÎº¥À» ºÒ·¯¿Â´Ù.
        InitializeQuest();
        base.ifQuestBtnClicked();
        highlight.SetActive(false);
    }

    protected override void InitializeQuest()
    {
        base.InitializeQuest();
        GetQuestDialogue("인게임 대사", "법학강의실", "Rsub2");
    }
}
