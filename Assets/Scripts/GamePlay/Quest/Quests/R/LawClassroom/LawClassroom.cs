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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        questManager = QuestManager.QuestManager_instance;
        //inventoryManager = InventoryManager.InvenManager_Instance;
        //inventoryManager.SetItemsOnInven(slotList);

        questName = "LawClassroom_SubQuest";
        leftAnchorY = leftScale.GetComponent<RectTransform>().anchoredPosition.y;
        rightAnchorY = rightScale.GetComponent<RectTransform>().anchoredPosition.y;
    }
    void Update()
    {
        //fix scales' rotation z
        leftScale.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        rightScale.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);

        //fix left scales' pos x and relative pos y
        Vector3 newPos = leftScale.GetComponent<RectTransform>().position;
        newPos.x = leftAnchor.position.x;
        newPos.y = leftAnchor.position.y + leftAnchorY;
        leftScale.GetComponent<RectTransform>().position = newPos;

        //fix left scales' pos x and relative pos y
        newPos = rightScale.GetComponent<RectTransform>().position;
        newPos.x = rightAnchor.position.x;
        newPos.y = rightAnchor.position.y + rightAnchorY;
        rightScale.GetComponent<RectTransform>().position = newPos;
    }
    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        inventoryManager = InventoryManager.InvenManager_Instance;
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            ifQuestBtnClicked();
            Debug.Log(questName + " 시작");
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
        inventoryManager.ObtainItem("RustedSword");
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
        target = 4 + (leftScale.sum - rightScale.sum) * angle;
        if (target < -16) target = -16;
        else if (target > 16) target = 16;

        float time = 0;
        Quaternion origin = topScale.rotation;
        //Debug.Log(origin.eulerAngles.z + " -> " + target);

        while (time < 1f)
        {
            if ((leftScale.sum - rightScale.sum) != 0)
                time += Time.deltaTime / Mathf.Abs(leftScale.sum - rightScale.sum);
            else time += Time.deltaTime;

            topScale.rotation = Quaternion.Lerp(origin, Quaternion.Euler(0f, 0f, target), time);
            yield return null;
        }

        if (CheckCompletion()) OnQuestCompleted();
        //Debug.Log("changed : " + topScale.rotation.eulerAngles.z);
    }
    public override void ifQuestBtnClicked()
    {//quest버튼이 눌렸을 때마다 실행되는 함수. 여기서는 인벤을 불러온다.
        base.ifQuestBtnClicked();
    }
}
