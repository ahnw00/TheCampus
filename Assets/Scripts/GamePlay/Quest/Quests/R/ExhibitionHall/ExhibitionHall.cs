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
    {//����Ʈ�� �����Ҷ� ����
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            inventoryManager = InventoryManager.InvenManager_Instance;
        }
        SetItemsOnInven();
    }
    public override void ifQuestBtnClicked() 
    {//����Ʈ ���� �� ���� inven fetch
        foreach(var slot in slotList)
        {//����Ʈ �г� inven initialize
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
