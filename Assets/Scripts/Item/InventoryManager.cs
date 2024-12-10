using NUnit.Framework;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private DataManager dataManager;
    private SaveDataClass saveData;
    private static InventoryManager instance = null;
    [SerializeField] private GameObject inventory;
    public ItemClass LastClickedItem {  get; private set; }

    [SerializeField] private List<ItemSlot> slotList = new List<ItemSlot>();
    private List<string> itemList { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("MapManager duplicate error, destroying duplicate instance.");
            //Destroy(this.gameObject);
        }
        //isInvenOpened = inventory.activeSelf;

        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
        itemList = saveData.itemList;

        SetItemsOnInven(slotList);
    }

    public void SetItemsOnInven(List<ItemSlot> slotList)
    {
        GameObject prefab;
        string path;
        int slotIdx = 0;
        foreach (var item in itemList)
        {
            path = "Prefabs/Item/" + item;
            prefab = Resources.Load<GameObject>(path);
            prefab = Instantiate(prefab, slotList[slotIdx].transform);
            slotList[slotIdx].curItem = prefab.GetComponent<ItemClass>();
            slotIdx++;
        }
    }

    //function that called when inven closed
    public void SaveItems() //reset item list and save, then set items on inven
    {
        saveData.itemList.Clear();
        foreach(var slot in slotList)
        {
            if(slot.curItem)
            {
                string temp = slot.curItem.name.Replace("(Clone)", "");
                saveData.itemList.Add(temp);
                Destroy(slot.curItem.gameObject);
            }
        }
        dataManager.Save();
        SetItemsOnInven(slotList);
        GameManager.GameManager_Instance.isUiOpened = false;
    }

    public void SetLastClickedItem(ItemClass item)
    {
        if (item == null) return;
        LastClickedItem = item;
        //Debug.Log($"last clicked item updated : {item.name}");
    }

    public static InventoryManager InvenManager_Instance
    {
        get
        {
            if (null == instance)
            {
                Debug.Log("InvenManager is null");
                return null;
            }
            return instance;
        }
    }
}
