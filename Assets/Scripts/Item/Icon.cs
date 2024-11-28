using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public Item itemData; // 실제 아이템의 데이터

    public void SetItem(Item newItem)
    {
        itemData = newItem;
        GetComponent<Image>().sprite = newItem.Icon;
    }
}
