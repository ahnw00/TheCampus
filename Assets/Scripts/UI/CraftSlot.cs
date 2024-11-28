using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IDropHandler
{
    private Item curItem;

    public void SetItem(Item item)
    {
        curItem = item;
        GetComponent<Icon>().SetItem(item);
    }

    public Item GetItem() { return curItem; }
    public void RemoveItem() { 
        curItem = null;
        GetComponent<Icon>().SetItem(null);
    }
    public void OnDrop(PointerEventData eventData)
    {
        // 드롭된 오브젝트가 DragDrop 컴포넌트를 가지고 있어야 함
        DragDrop draggedItem = eventData.pointerDrag.GetComponent<DragDrop>();

        if (draggedItem != null)
        {
            // 부모를 변경(드롭된 슬롯으로)
            draggedItem.transform.SetParent(transform);
            draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // 슬롯 중앙 정렬
            Debug.Log($"아이템이 {gameObject.name} 슬롯에 드롭됨");
        }
    }
}
