using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System.IO;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class ObtainableItem : Clickable
{
    protected DataManager dataManager;
    protected SaveDataClass data;
    protected InventoryManager inventoryManager;
    protected GameManager gameManager;
    protected string itemKey;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        dataManager = DataManager.Instance;
        data = dataManager.saveData;
        inventoryManager = InventoryManager.InvenManager_Instance;
        gameManager = GameManager.GameManager_Instance;

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
                printDialogue();
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
        string path = "Prefabs/Item/" + this.name;
        GameObject prefab = Resources.Load<GameObject>(path);
        inventoryManager.itemObtainImage.sprite = prefab.GetComponent<Image>().sprite;

        //텍스트
        List<string> itemInformation = new List<string>();
        itemInformation = DialogueManager.DialoguManager_Instance.GetItemDiscription(this.name);
        inventoryManager.itemObtainName.SetText(itemInformation[0]);
        inventoryManager.itemObtainInputField.SetText(itemInformation[1]);
    }

    protected virtual void ObtainItem()
    {
        bool _flag = false;
        if(this.name == "Broadcast")
        {
            gameManager.TurnOnRadio();
            inventoryManager.itemObtainPanel.SetActive(false);
            return;
        }
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
                TextManager.TextManager_Instance.PopUpText(DialogueManager.DialoguManager_Instance.GetItemDiscription(this.name)[0] + DialogueManager.DialoguManager_Instance.GetSystemDialogue("시스템 대사", 0));
                break;
            }
        }
        if (!_flag)
        {
            TextManager.TextManager_Instance.PopUpText("인벤토리가 가득 찼다.");
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

    void printDialogue()
    {
        //대사 출력 및 todolist작성
        if (PlayerPrefs.HasKey("pieceTrigger"))
        {
            Debug.Log("already started");
        }
        else
        {
            List<string> list = DialogueManager.DialoguManager_Instance.GetIngameDialogue("인게임 대사", "R동 전체", "Rmain");
            TextManager.TextManager_Instance.PopUpText(list[0]);
            Record.Record_Instance.AddText(TextType.Speaker, list[0], "나");
            LocationAndTodoList.LocationAndTodoList_Instance.SetTodo("Rmain", DialogueManager.DialoguManager_Instance.GetQuestDialogue("퀘스트 대사", "R동 전체", "Rmain"));
            PlayerPrefs.SetInt("pieceTrigger", 1);
            PlayerPrefs.Save();
        }
    }
}
