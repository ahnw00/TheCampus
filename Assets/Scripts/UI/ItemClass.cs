using UnityEngine;

public class ItemClass : MonoBehaviour
{
    public ItemSlot detectedSlot;
    private Vector2 originPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPos = this.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag()
    {
        this.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnPointerUp()
    {
        if(detectedSlot)
        {
            Vector2 slotPos = detectedSlot.GetComponent<RectTransform>().position;
            this.GetComponent<RectTransform>().position = slotPos;
        }
        else
        {
            this.GetComponent<RectTransform>().position = originPos;
        }
    }
}
