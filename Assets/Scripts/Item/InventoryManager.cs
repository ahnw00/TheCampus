﻿using NUnit.Framework;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Linq;
using static UnityEditor.Progress;
using System;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private DataManager dataManager;
    private SaveDataClass saveData;
    private TextManager textManager;
    private static InventoryManager instance = null;
    
    [SerializeField] private GameObject inventory;

    [SerializeField] GameObject LastClickedItemObj; //인벤토리 버튼 옆 이미지 오브젝트
    [SerializeField] GameObject selectedItemHighlight; // 실제로 클릭된 아이템 오브젝트
    [SerializeField] List<GameObject> questInvenSlotImageList; // quest에서 사용하는 인벤토리의 슬롯 이미지 리스트, 하이라이트 표시용
    [SerializeField] List<GameObject> questInvenSlotList; // 퀘스트창의 인벤토리
    private string selectedItemName; // 선택된 아이템의 이름을 저장

    public List<ItemSlot> slotList = new List<ItemSlot>(); // 인벤토리 버튼을 눌렀을 때 나오는 인벤토리 슬롯들
    public List<string> itemList; // savedata에 저장되어있는 아이템 리스트들
    [SerializeField] List<ItemSlot> craftSlotList = new List<ItemSlot>(); // 제작대 슬롯 리스트
    [SerializeField] GameObject resultSlot; // 아이템 합성 결과물 슬롯
    private Dictionary<Tuple<string, string>, string> craftRecipe = new Dictionary<Tuple<string, string>, string>(); // 조합법 사전

    public GameObject itemObtainPanel;
    public Image itemObtainImage;
    public TextMeshProUGUI itemObtainInputField;
    public Button itemObtainBtn;

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
        textManager = TextManager.TextManager_Instance;
        CraftRecipeSet();//조합법등록
        SetItemsOnInven();
    }

    public void ObtainItem(string itemName)
    {
        bool _flag = false;
        foreach (var slot in slotList)
        {
            if (slot.curItem == null)
            {
                saveData.itemList.Add(itemName);
                string path = "Prefabs/Item/" + itemName;
                GameObject prefab = Resources.Load<GameObject>(path);
                prefab = Instantiate(prefab, slot.transform);
                slot.curItem = prefab.GetComponent<ItemClass>();
                //this.gameObject.SetActive(false);
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
            if(itemObtainPanel.activeSelf == true)
                itemObtainPanel.SetActive(false);
        }
    }

    public void SetItemsOnInven(List<ItemSlot> _slotList = null)
    {
        if (_slotList == null)
            _slotList = slotList;
        GameObject prefab;
        string path;
        int slotIdx = 0;
        foreach (var item in itemList)
        {
            path = "Prefabs/Item/" + item;
            prefab = Resources.Load<GameObject>(path);
            prefab = Instantiate(prefab, _slotList[slotIdx].transform);
            _slotList[slotIdx].curItem = prefab.GetComponent<ItemClass>();
            slotIdx++;
        }
    }

    //function that called when inven closed
    //매개변수가 있으면 button에 함수가 안떠
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
        // 조합대에 올려놓고 인벤을 닫는경우 인벤토리에 원위치 시키는 함수
        foreach (var slot in craftSlotList)
        {
            if (slot.curItem)
            {
                string temp = slot.curItem.name.Replace("(Clone)", "");
                saveData.itemList.Add(temp);
                Destroy(slot.curItem.gameObject);
            }
        }

        if (resultSlot.GetComponent<ResultSlot>().curItem != null)
        {
            Destroy(resultSlot.GetComponent<ResultSlot>().curItem.gameObject);
        }
        dataManager.Save();
        SetItemsOnInven();
        GameManager.GameManager_Instance.isUiOpened--;
    }

    public void SetLastClickedItem(ItemClass item)
    {
        if (item == null) return; //클릭한 아이템이 없으면 return

        //클릭한 이미지와 아이템의 이름을 인벤토리 옆에 표시
        LastClickedItemObj.GetComponent<Image>().sprite = item.gameObject.GetComponent<Image>().sprite;
        selectedItemName = item.name.Replace("(Clone)", "");

        //선택한 아이템의 슬롯 하이라이트 활성화
        if (selectedItemName == null)
        {//선택된 아이템이 없다면 하이라이트 비활성화
            selectedItemHighlight.SetActive(false);
            return;
        }
        selectedItemHighlight.SetActive(true);

        // 인벤토리에서 클릭했을때 하이라이트 || 퀘스트인벤에서 클릭했을 때 하이라이트
        for (int i = 0; i < slotList.Count; i++) 
        {
            if (slotList[i] == item.originSlot || questInvenSlotList[i] == item.originSlot)
            {
                selectedItemHighlight.transform.SetParent(questInvenSlotImageList[i].transform, false);
                selectedItemHighlight.transform.localPosition = Vector3.zero;
                break;
            }
        }

        /*
        int i = 0;
        foreach (var slot in slotList)
        {
            if (slot == item.originSlot || )
            {
                
            }
            i++;
        }
        */
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
    
    public void CraftItem() //조합대에 아이템이 올라올때마다 실행
    {
        //craftslot에 모두 아이템이 올라와 있는 경우
        if (craftSlotList[0].curItem != null && craftSlotList[1].curItem != null)
        {
            string combinedItem, ingredient1, ingredient2;
            ingredient1 = craftSlotList[0].curItem.gameObject.name.Replace("(Clone)", "");
            ingredient2 = craftSlotList[1].curItem.gameObject.name.Replace("(Clone)", "");
            combinedItem = TryCombinedItem(ingredient1, ingredient2);
            if (combinedItem != null) 
            { //조합대에 넣은 아이템의 조합법이 있는경우 resultSlot에 아이템 생성
                GameObject prefab;
                string path;
                path = "Prefabs/Item/" + combinedItem;
                prefab = Resources.Load<GameObject>(path);
                resultSlot.GetComponent<ResultSlot>().curItem = Instantiate(prefab, resultSlot.transform);
            }
            else if(combinedItem == null && resultSlot.GetComponent<ResultSlot>().curItem)
            {//craftSlot의 item이 없는 recipe로 교체 resultSlot에 있던 아이템 파괴
                resultSlot.GetComponent<ResultSlot>().curItem = null;
                Destroy(resultSlot.transform.GetChild(0).gameObject);
            }
        }
        else 
        {//craftSlot의 item 제거 등으로 recipe가 없는경우 resultSlot에 있던 아이템 파괴
            if (resultSlot.GetComponent<ResultSlot>().curItem && resultSlot.transform.childCount > 0)
            {
                resultSlot.GetComponent<ResultSlot>().curItem = null;
                Destroy(resultSlot.transform.GetChild(0).gameObject);
            }
        }
    }

    private string TryCombinedItem(string item1, string item2)
    {//조합법이 있는지 검사, 있으면 완성아이템 반환
        string resultItem = null;
        //순서에 상관없게끔 둘다 검사
        var key1 = new Tuple<string, string> (item1, item2);
        var key2 = new Tuple<string, string>(item2, item1);
        if (craftRecipe.ContainsKey(key1))
        {
            resultItem = craftRecipe[key1];
        }
        else if (craftRecipe.ContainsKey(key2))
        {
            resultItem = craftRecipe[key2];
        }
        return resultItem;
    }
    private void CraftRecipeSet()
    {//여기에 조합법 작성
        craftRecipe.Add(new Tuple<string, string>("TearedPaperCup", "Tape"), "FixedPaperCup");
        craftRecipe.Add(new Tuple<string, string>("FixedPaperCup", "WoodStick" ), "HandyLadle");
        craftRecipe.Add(new Tuple<string, string>("OldFlashlight", "2Vbattery"), "LowBatteryFlashlight");
        craftRecipe.Add(new Tuple<string, string>("LowBatteryFlashlight", "2Vbattery"), "Flashlight");
    }

    public void DestoryIngredients()
    {//완성아이템을 inventorySlot으로 옮기는경우 조합아이템들 파괴
        foreach(var slot in craftSlotList)
        {
            Destroy(slot.transform.GetChild(0).gameObject);
        }
    }

    public string GetSelectedItemName()
    {
        if(selectedItemName == null)
        {
            return "null";
        }
        return selectedItemName;
    }

    public void SetUiOpened()
    {
        GameManager.GameManager_Instance.TurnOnUI();
    }
}
