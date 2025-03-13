using System.Collections.Generic;
using UnityEngine;

public class CabinetManager : MonoBehaviour
{
    private DataManager dataManager;
    private SaveDataClass saveData;
    private static CabinetManager instance;

    [SerializeField] private List<ItemSlot> cabinetSlotList = new List<ItemSlot>();
    [SerializeField] private List<string> cabinetList; // savedata에 저장되어있는 캐비닛 리스트들

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }

        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
        cabinetList = saveData.cabinetList;
        SetItemsOnCabinet();
    }

    public void SetItemsOnCabinet(List<ItemSlot> _slotList = null)
    {
        if (_slotList == null)
            _slotList = cabinetSlotList;
        GameObject prefab;
        string path;
        int slotIdx = 0;
        foreach (var item in cabinetList)
        {
            path = "Prefabs/Item/" + item;
            prefab = Resources.Load<GameObject>(path);
            prefab = Instantiate(prefab, _slotList[slotIdx].transform);
            _slotList[slotIdx].curItem = prefab.GetComponent<ItemClass>();
            slotIdx++;
        }
    }

    public void SaveItemsOnCabinet() //reset item list and save, then set items on inven
    {
        saveData.cabinetList.Clear();
        foreach (var slot in cabinetSlotList)
        {
            if (slot.curItem)
            {
                string temp = slot.curItem.name.Replace("(Clone)", "");
                saveData.cabinetList.Add(temp);
                Destroy(slot.curItem.gameObject);
            }
        }

        dataManager.Save();
        SetItemsOnCabinet();
    }

    public static CabinetManager CabinetManager_Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
}
