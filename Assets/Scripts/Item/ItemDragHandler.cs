using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour
{
    private Transform originalParent; // 원래 부모
    private Vector2 originalPosition; // 원래 위치
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    public void OnBeginDrag()
    {
        originalParent = transform.parent;
        originalPosition = GetComponent<RectTransform>().anchoredPosition; // 돌아갈 위치 저장
        canvasGroup.blocksRaycasts = false; // raycast 비활성화
        canvasGroup.alpha = 0.7f; // 이동 시 살짝 투명하게
        transform.SetParent(canvas.transform); // canvas 최상위로 이동
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += pointerData.delta / canvas.scaleFactor; 
    }

    public void OnEndDrag()
    {
        canvasGroup.blocksRaycasts = true; // raycast 재활성화
        canvasGroup.alpha = 1f; // 투명도 복구
        transform.SetParent(originalParent); // 부모 복구
        GetComponent<RectTransform>().anchoredPosition = originalPosition; // 위치 복구
    }
}
