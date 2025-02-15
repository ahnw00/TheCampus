using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenObtainableItem : ObtainableItem
{
    [SerializeField] string requiredItem;
    private bool hiddenFlag = true;
    public override void PopUpObtainPanel()
    {// ���õ� �������� �ʿ��� �������� ���� ���� �� �ִ�.
        if (inventoryManager.GetSelectedItemName() == requiredItem)
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

            if (hiddenFlag)
            {
                string flashlightDialogue = DialogueManager.DialoguManager_Instance.GetSystemDialogue("인게임 대사", 2);
                TextManager.TextManager_Instance.PopUpText(flashlightDialogue);
                Record.Record_Instance.AddText(TextType.Speaker, flashlightDialogue, "나");
                hiddenFlag = false;
            }
        }
        else
        {
            TextManager.TextManager_Instance.PopUpText(DialogueManager.DialoguManager_Instance.GetSystemDialogue("시스템 대사", 3));
        }
    }

    protected override void ObtainItem()
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
                PlayerPrefs.SetInt(itemKey, 1);
                PlayerPrefs.Save();
                GetPiece();
                break;
            }
        }
        if (!_flag)
        {
            TextManager.TextManager_Instance.PopUpText("Inventory is full");
        }
        else
        {
            inventoryManager.itemObtainPanel.SetActive(false);
        }
    }
    protected void GetPiece()
    {
        PlayerPrefs.SetInt("Piece2", 1);
        PlayerPrefs.Save();
    }
}
