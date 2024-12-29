using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitionHall : Quest
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject flashLight;
    [SerializeField] GameObject picture;
    [SerializeField] GameObject hiddenPicture;
    [SerializeField] List<ItemSlot> slotList;
    [SerializeField] private GameObject questInven;
    bool pictureFlag = true;
    bool hiddenPictureFlag = true;
    bool isGetPicture = false;
    public override void Start()
    {
        questName = "ExhibitionHall_SubQuest";
        requiredItems = null;
    }

    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            inventoryManager = InventoryManager.InvenManager_Instance;
        }
        inventoryManager.SetItemsOnInven(slotList);
    }

    public override void ifQuestBtnClicked()
    {//quest버튼이 눌렸을 때마다 실행되는 함수. 여기서는 인벤을 불러온다.
        questInven.SetActive(true);
        foreach (var slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }
        inventoryManager.SetItemsOnInven(slotList);
    }
    protected override void OnQuestCompleted()
    {
        Debug.Log(questName + "clear");
        Debug.Log($"item get");
    }

    public void OnPictureClicked()
    {
        if(inventoryManager.GetSelectedItemName() == "Flashlight" && pictureFlag)
        {
            pictureFlag = false;
            flashLight.SetActive(true);
            flashLight.transform.SetParent(picture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
            Invoke("TurnOffLight", 1f);
        }
    }
    public void OnHiddenPictureClicked()
    {
        if (inventoryManager.GetSelectedItemName() == "Flashlight" && hiddenPictureFlag)
        {
            hiddenPictureFlag = false;
            flashLight.SetActive(true);
            flashLight.transform.SetParent(hiddenPicture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
        }
        else if(inventoryManager.GetSelectedItemName() == "RustedSword" && !hiddenPictureFlag && !isGetPicture)
        {
            isGetPicture = true;
            hiddenPicture.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/temp/tempPicture");
        }
    }
    void TurnOffLight()
    {
        flashLight.SetActive(false);
    }
}
