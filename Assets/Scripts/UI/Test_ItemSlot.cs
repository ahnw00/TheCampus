using UnityEngine;

public class Test_ItemSlot : MonoBehaviour
{
    private GameObject currentItem; // 현재 슬롯에 있는 아이템

    void OnTriggerEnter2D(Collider2D other)
    {
        // 아이템 감지
        if (other.CompareTag("Item"))
        {
            currentItem = other.gameObject;
            Debug.Log($"Item entered {gameObject.name}");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 슬롯을 벗어나는 아이템 감지
        if (other.CompareTag("Item"))
        {
            if (currentItem == other.gameObject)
            {
                currentItem = null;
                Debug.Log($"Item exited {gameObject.name}");
            }
        }
    }

    public GameObject GetCurrentItem()
    {
        return currentItem; // 현재 슬롯에 있는 아이템 반환
    }
}
