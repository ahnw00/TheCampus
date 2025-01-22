using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawClassroom : Quest
{
    private QuestManager questManager;
    [SerializeField] public Scale leftScale, rightScale;
    [SerializeField] private RectTransform leftAnchor, rightAnchor;
    [SerializeField] private RectTransform topScale;
    private float leftAnchorY, rightAnchorY;
    private float target;
    private float startRot = 4f;
    public bool isCoroutineRunning = false;
    [SerializeField] private GameObject rustedSword;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        questManager = QuestManager.QuestManager_instance;
        //inventoryManager = InventoryManager.InvenManager_Instance;
        //inventoryManager.SetItemsOnInven(slotList);

        questName = "LawClassroom_SubQuest";
        topScale.localRotation = Quaternion.Euler(0, 0, startRot);
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
        inventoryManager = InventoryManager.InvenManager_Instance;
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            ifQuestBtnClicked();
            Debug.Log(questName + " ½ÃÀÛ");
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
        Debug.Log(questName + "clear");
        //inventoryManager.ObtainItem("RustedSword");
        rustedSword.SetActive(true);
        questStatus = QuestStatus.Completed;
        questManager.SaveQuestStatus();
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
        base.ifQuestBtnClicked();
    }
}
