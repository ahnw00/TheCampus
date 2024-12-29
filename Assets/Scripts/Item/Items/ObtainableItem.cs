using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System.IO;
using UnityEngine.UI;

public class ObtainableItem : Clickable
{
    DataManager dataManager;
    SaveDataClass data;
    InventoryManager inventoryManager;
    TextManager textManager;
    //{item, 최대 소유 가능 개수}, 2개 이상인 것들만 이 리스트에 넣은거
    //private Dictionary<string, int> multipleItems = new Dictionary<string, int>()
    //{
    //    {"2Vbattery", 2},
    //    {"OldCan", 2}
    //};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataManager = DataManager.Instance;
        data = dataManager.saveData;
        inventoryManager = InventoryManager.InvenManager_Instance;
        textManager = TextManager.TextManager_Instance;

        //인벤토리의 itemList에 이 아이템이 있다면 월드맵 위에 있는건 setActive(false) 해줘.
        //string itemName = this.name;
        //if (multipleItems.ContainsKey(itemName))
        //{
        //    if (data.itemList.Where(item => item == itemName).Count() == multipleItems[itemName])
        //        this.gameObject.SetActive(false);
        //}
        //if (data.itemList.Find(item => item == this.name) != null)
        //    this.gameObject.SetActive(false);
    }

    public override void Clicked()
    {
        base.Clicked();
        if (flag == 1)
        {
            PopUpObtainPanel();
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

    void PopUpObtainPanel()
    {
        inventoryManager.itemObtainPanel.SetActive(true);
        GameManager.GameManager_Instance.isUiOpened = true;
        inventoryManager.itemObtainBtn.onClick.RemoveAllListeners();
        inventoryManager.itemObtainBtn.onClick.AddListener(ObtainItem);
        //아이템 획득 패널에서의 아이템 이미지랑 텍스트 세팅해줘야해
        //
        //
    }

    void ObtainItem()
    {
        bool _flag = false;
        foreach (var slot in inventoryManager.slotList)
        {
            if (slot.curItem == null)
            {
                data.itemList.Add(this.name);
                string path = "Prefabs/Item/" + this.name;
                GameObject prefab = Resources.Load<GameObject>(path);
                prefab = Instantiate(prefab, slot.transform);
                slot.curItem = prefab.GetComponent<ItemClass>();
                this.gameObject.SetActive(false);
                dataManager.Save();
                _flag = true;
                break;
            }
        }
        if (!_flag)
        {
            textManager.PopUpText("Inventory is full");
        }
        else
        {
            GameManager.GameManager_Instance.isUiOpened = false;
            inventoryManager.itemObtainPanel.SetActive(false);
        }
    }
}
