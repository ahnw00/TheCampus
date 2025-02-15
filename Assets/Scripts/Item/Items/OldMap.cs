using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldMap : Clickable
{
    DataManager dataManager;
    SaveDataClass data;
    InventoryManager inventoryManager;
    [SerializeField] GameObject MiniMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataManager = DataManager.Instance;
        data = dataManager.saveData;
        inventoryManager = InventoryManager.InvenManager_Instance;

        if (data.isMapObtained)
        {
            MiniMap.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public override void Clicked()
    {
        base.Clicked();
        if (flag == 1)
        {
            MapClicked();
        }
        else Invoke("Delayed", searchingTime + 0.05f);
    }

    void Delayed()
    {
        if(flag == 1)
        {
            PopUpObtainPanel();
        }
    }

    void MapClicked()
    {
        MiniMap.SetActive(true);
        data.isMapObtained = true;
        dataManager.Save();
        inventoryManager.itemObtainPanel.SetActive(false);
        LocationAndTodoList.LocationAndTodoList_Instance.SetLocation(MapManager.MapManager_Instance.cur_node.node_name);
        this.gameObject.SetActive(false);
    }

    public virtual void PopUpObtainPanel()
    {
        inventoryManager.itemObtainPanel.SetActive(true);
        GameManager.GameManager_Instance.TurnOnUI();
        inventoryManager.itemObtainBtn.onClick.RemoveAllListeners();
        inventoryManager.itemObtainBtn.onClick.AddListener(MapClicked);
        //아이템 획득 패널에서의 아이템 이미지랑 텍스트 세팅해줘야해
        string path = "Prefabs/Item/" + this.name;
        GameObject prefab = Resources.Load<GameObject>(path);
        inventoryManager.itemObtainImage.sprite = prefab.GetComponent<Image>().sprite;

        //텍스트
        List<string> itemInformation = new List<string>();
        itemInformation = DialogueManager.DialoguManager_Instance.GetItemDiscription(this.name);
        inventoryManager.itemObtainName.SetText(itemInformation[0]);
        inventoryManager.itemObtainInputField.SetText(itemInformation[1]);
    }
}
