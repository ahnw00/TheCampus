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
            inventoryManager.itemObtainBtn.onClick.AddListener(GetPiece);
            //������ ȹ�� �гο����� ������ �̹����� �ؽ�Ʈ �����������
            //
            //
        }
    }

    protected void GetPiece()
    {
        PlayerPrefs.SetInt("Piece2", 1);
        PlayerPrefs.Save();
    }
}
