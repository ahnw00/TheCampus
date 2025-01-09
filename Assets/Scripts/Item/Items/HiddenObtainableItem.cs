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
}
