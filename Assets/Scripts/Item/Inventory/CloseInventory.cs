using UnityEngine;
using UnityEngine.EventSystems;

public class CloseInventory : MonoBehaviour
{
    public GameObject inventoryOverlay;
    public GameObject inventory; // InventoryOverlay ������Ʈ

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            // Pointer�� UI ��� ���� �ִ��� Ȯ��
            if (!IsPointerNotOverInventory(inventory))
            {
                //Debug.Log("Background Ŭ��: â �ݱ�");
                inventoryOverlay.SetActive(false); // Inventory �ݱ�
            }
            else
            {
                //Debug.Log("Inventory Ŭ��: â ����");
            }
        }
    }

    // Pointer�� UI ��� ���� �ִ��� Ȯ���ϴ� �Լ�
    private bool IsPointerNotOverInventory(GameObject targetUI)
    {
        // ���� ���콺 ��ġ���� Raycast�� ������ UI ��ҿ� ��Ҵ��� Ȯ��
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