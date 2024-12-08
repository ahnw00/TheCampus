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
    //public bool isInvenOpened;
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

        SetItemsOnInven();
    }

    public void SetItemsOnInven()
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

    //인벤토리를 닫을 때 실행되는 함수
    public void SaveItems() //아이템 리스트를 초기화하면서 저장 후 인벤에 올려놓음
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
        SetItemsOnInven();
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
                Debug.Log("MapManager is null");
                return null;
            }
            return instance;
        }
    }
}
