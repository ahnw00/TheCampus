using UnityEngine;

public class HiddenObtainableItem : ObtainableItem
{
    [SerializeField] string requiredItem;
    public override void PopUpObtainPanel()
    {// 선택된 아이템이 필요한 아이템일 때만 얻을 수 있다.
        if (inventoryManager.GetSelectedItemName() == requiredItem)
        {
            inventoryManager.itemObtainPanel.SetActive(true);
            GameManager.GameManager_Instance.TurnOnUI();
            inventoryManager.itemObtainBtn.onClick.RemoveAllListeners();
            inventoryManager.itemObtainBtn.onClick.AddListener(GetPiece);
            //아이템 획득 패널에서의 아이템 이미지랑 텍스트 세팅해줘야해
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
