using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ExhibitionHall : Quest
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject flashLight;
    [SerializeField] GameObject picture;
    [SerializeField] GameObject hiddenFicture;
    [SerializeField] List<ItemSlot> slotList;
    public override void Start()
    { 
        questName = "ExhibitionHall_SubQuest";
        requiredItems = null;
    }

    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            inventoryManager = InventoryManager.InvenManager_Instance;
        }
        SetItemsOnInven();
    }
    public override void ifQuestBtnClicked() 
    {//퀘스트 실행 시 마다 inven fetch
        foreach(var slot in slotList)
        {//퀘스트 패널 inven initialize
            if(slot.transform.GetChild(0) != null)
            {
                Destroy(slot.transform.GetChild(0));
            }
        }

        SetItemsOnInven();
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
        Debug.Log($"item get");
    }

    public void OnPaintClicked()
    {
        //if(inventoryManager.LastClickedItem == )
    }
    private void SetItemsOnInven(List<ItemSlot> _slotList = null)
    {
        if (_slotList == null)
            _slotList = slotList;
        GameObject prefab;
        string path;
        int slotIdx = 0;
        foreach (var item in inventoryManager.itemList)
        {
            path = "Prefabs/Item/" + item;
            prefab = Resources.Load<GameObject>(path);
            prefab = Instantiate(prefab, _slotList[slotIdx].transform);
            _slotList[slotIdx].curItem = prefab.GetComponent<ItemClass>();
            slotIdx++;
        }
    }
}
