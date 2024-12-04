using NUnit.Framework;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private SaveDataClass saveData;
    private static InventoryManager instance = null;
    [SerializeField] private GameObject inventory;
    public bool isInvenOpened;
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
        isInvenOpened = inventory.activeSelf;
        saveData = DataManager.Instance.saveData;
        itemList = saveData.itemList;

        GameObject prefab;
        string path;
        int slotIdx = 0;
        foreach(var item in itemList)
        {
            path = "Prefabs/Item/" + item;
            prefab = Resources.Load<GameObject>(path);
            prefab = Instantiate(prefab, slotList[slotIdx].transform) as GameObject;
            slotList[slotIdx].curItem = prefab.GetComponent<ItemClass>();
            slotIdx++;
        }
    }

    public void CheckInvenOpened()
    {
        if(!isInvenOpened) isInvenOpened = true;
        else isInvenOpened = false;
    }

    public void CloseInventory()
    {

    }

    public void SetLastClickedItem(ItemClass item)
    {
        if (item == null) return;
        LastClickedItem = item;
        Debug.Log($"last clicked item updated : {item.name}");
    }

    public static InventoryManager InvenManager_Instance
    {
        get
        {
            if (null == instance)
            {
                Debug.Log("MapManager is null");
                return null;
            }
            return instance;
        }
    }
}
