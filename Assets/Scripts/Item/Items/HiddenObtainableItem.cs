using UnityEngine;

public class HiddenObtainableItem : ObtainableItem
{
    [SerializeField] string requiredItem;
    public override void PopUpObtainPanel()
    {// ���õ� �������� �ʿ��� �������� ���� ���� �� �ִ�.
        if (inventoryManager.GetSelectedItemName() == requiredItem)
        {
            inventoryManager.itemObtainPanel.SetActive(true);
            GameManager.GameManager_Instance.TurnOnUI();
            inventoryManager.itemObtainBtn.onClick.RemoveAllListeners();
            inventoryManager.itemObtainBtn.onClick.AddListener(ObtainItem);
            //������ ȹ�� �гο����� ������ �̹����� �ؽ�Ʈ �����������
            //
            //
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
            textManager.PopUpText("Inventory is full");
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
