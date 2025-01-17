using UnityEngine;
using UnityEngine.EventSystems;

public class CloseInventory : MonoBehaviour
{
    public GameObject inventoryOverlay;
    public GameObject inventory; // InventoryOverlay 오브젝트

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            // Pointer가 UI 요소 위에 있는지 확인
            if (!IsPointerNotOverInventory(inventory))
            {
                //Debug.Log("Background 클릭: 창 닫기");
                inventoryOverlay.SetActive(false); // Inventory 닫기
            }
            else
            {
                //Debug.Log("Inventory 클릭: 창 유지");
            }
        }
    }

    // Pointer가 UI 요소 위에 있는지 확인하는 함수
    private bool IsPointerNotOverInventory(GameObject targetUI)
    {
        // 현재 마우스 위치에서 Raycast를 실행해 UI 요소에 닿았는지 확인
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach(var result in results)
        {
            if (result.gameObject == targetUI)
            {
                return true;
            }
        }
        return false;
    }
}