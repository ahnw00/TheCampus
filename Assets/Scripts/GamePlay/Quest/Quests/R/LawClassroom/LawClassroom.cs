using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawClassroom : Quest
{
    [SerializeField] private List<ItemSlot> slotList = new List<ItemSlot>();
    [SerializeField] public Scale leftScale, rightScale;
    [SerializeField] private RectTransform leftAnchor, rightAnchor;
    [SerializeField] private RectTransform topScale;
    private float leftAnchorY, rightAnchorY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        inventoryManager = InventoryManager.InvenManager_Instance;
        inventoryManager.SetItemsOnInven(slotList);

        questName = "LawClassroom_SubQuest";
        requiredItems = null;
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
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            Debug.Log(questName + " 시작");
        }
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
    }

    public IEnumerator ScaleCoroutine()
    {
        float angle = 4;
        float target = (leftScale.sum - rightScale.sum) * angle;
        float time = 0;
        Quaternion origin = topScale.rotation;
        //Debug.Log(origin.eulerAngles.z + " -> " + target);

        while (time < 1f)
        {
            if (target != 0)
                time += Time.deltaTime / Mathf.Abs(leftScale.sum - rightScale.sum);
            else time += Time.deltaTime;

            topScale.rotation = Quaternion.Lerp(origin, Quaternion.Euler(0f, 0f, target), time);
            yield return null;
        }

        if (target == 0) OnQuestCompleted();
        //Debug.Log("changed : " + topScale.rotation.eulerAngles.z);
    }
}
