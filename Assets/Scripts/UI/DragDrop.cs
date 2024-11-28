using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public Transform originalParent; // 드래그 전 부모 Transform
    private RectTransform rectTransform; // 드래그 대상
    private CanvasGroup canvasGroup; // raycast와 투명도 제어
    private Vector2 originalAnchoredPosition; // 잘못 드래그 시 다시 돌아갈 원래 위치

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); 
        canvasGroup = GetComponent<CanvasGroup>();  

        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 raycast 비활성화
        originalParent = transform.parent;
        originalAnchoredPosition = rectTransform.anchoredPosition;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f; // 드래그 시 살짝 투명하게
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중 마우스를 따라다니게
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시
        canvasGroup.blocksRaycasts = true; // 다시 raycast 활성화
        canvasGroup.alpha = 1f; // 다시 불투명하게

        GameObject target = eventData.pointerEnter; // 드랍 위치 확인

        // 드롭된 오브젝트가 슬롯이 아닐 경우 원래 위치로 돌아가게
        if (target != null && IsValidDrop(target))
        {
            // CraftResultSlot에서 드래그된 경우 추가 처리
            if (originalParent.GetComponent<CraftResultSlotManager>() != null)
            {
                CraftResultSlotManager craftResultSlot = originalParent.GetComponent<CraftResultSlotManager>();
                craftResultSlot.OnResultItemDraggedToItemSlot();
            }

            // 유효한 슬롯에 아이템 배치
            transform.SetParent(target.transform, false);
            rectTransform.anchoredPosition = Vector2.zero; // 슬롯 중앙 정렬
        }

        else
        {
            Debug.Log("유효하지 않은 드랍 위치");

            transform.SetParent(originalParent, false); // 부모 복원
            rectTransform.anchoredPosition = originalAnchoredPosition; // 원래 위치로 복귀
        }
    }

    private bool IsValidDrop(GameObject target)
    {
        // 드래그 시작 위치에 따라 유효한 드롭 위치 결정
        if (originalParent.GetComponent<CraftResultSlotManager>() != null)
        {
            // CraftResultSlot에서 드래그된 경우, ItemSlot만이 유효한 드랍 위치
            return target.GetComponent<ItemSlot>() != null;
        }

        else if (originalParent.GetComponent<ItemSlot>() != null || originalParent.GetComponent<CraftSlot>() != null)
        {
            // ItemSlot 또는 CraftSlot에서 드래그된 경우 ItemSlot과 CraftSlot 모두 유효한 드랍 위치
            return target.GetComponent<ItemSlot>() != null || target.GetComponent<CraftSlot>() != null;
        }

        return false; // 유효하지 않은 경우
    }
}
