using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System.IO;
using UnityEngine.UI;

public class ObtainableItem : Clickable
{
    protected DataManager dataManager;
    protected SaveDataClass data;
    protected InventoryManager inventoryManager;
    protected TextManager textManager;
    protected string itemKey;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        dataManager = DataManager.Instance;
        data = dataManager.saveData;
        inventoryManager = InventoryManager.InvenManager_Instance;
        textManager = TextManager.TextManager_Instance;

        itemKey = $"{this.gameObject.name}_{this.GetInstanceID()}"; //고유 ID생성

        if (PlayerPrefs.HasKey(itemKey))
        {//고유 ID를 가진 아이템이 월드맵에서 이미 획득되었다면 비활성화
            this.gameObject.SetActive(false);
        }
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

    protected void Delayed()
    {
        if(flag == 1)
        {
            if (this.name.Length == 6 && this.name.Substring(0, 5) == "Piece")
            {
                PlayerPrefs.SetInt(this.name, 1);
                PlayerPrefs.Save();
                this.gameObject.SetActive(false);
            }
            else
                PopUpObtainPanel();
        }
    }    

    public virtual void PopUpObtainPanel()
    {
        inventoryManager.itemObtainPanel.SetActive(true);
        GameManager.GameManager_Instance.TurnOnUI();
        inventoryManager.itemObtainBtn.onClick.RemoveAllListeners();
        inventoryManager.itemObtainBtn.onClick.AddListener(ObtainItem);
        //아이템 획득 패널에서의 아이템 이미지랑 텍스트 세팅해줘야해
        //
        //
    }

    protected virtual void ObtainItem()
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
                //고유 ID 아이템을 획득시 획득처리 저장
                PlayerPrefs.SetInt(itemKey, 1); 
                PlayerPrefs.Save();
                break;
            }
        }
        if (!_flag)
        {
            textManager.PopUpText("Inventory is full");
        }
        else
        {
            inventoryManager.itemObtainPanel.SetActive(false);
        }
    }

    public string GetItemKey()
    {
        return itemKey;
    }
}
