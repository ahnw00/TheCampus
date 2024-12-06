﻿using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClass : MonoBehaviour
{
    public ItemSlot detectedSlot; //감지된 슬롯(감지될 때마다 바뀜)
    public ItemSlot originSlot; //이동하기 전에 있었던 슬롯을 저장
    private Vector2 originPos = new Vector2(0, 0);

    public void OnPointerDown()
    {
        originSlot = detectedSlot;
    }

    public void OnDrag()
    {
        this.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnPointerUp() //포인터를 땠을 때
    {
        if(detectedSlot) //감지된 슬롯이 있다면
        {
            //감지된 슬롯 위에 아이템이 있고 그 아이템이 본인이 아니라면
            if (detectedSlot.curItem && detectedSlot.curItem != this)
            {
                //감지된 슬롯 위의 아이템을 현재의 아이템이 있던 슬롯으로 옮겨주는 과정
                ItemClass detectedItem = detectedSlot.curItem; //감지된 슬롯 위의 아이템
                detectedItem.transform.SetParent(originSlot.transform);
                detectedItem.GetComponent<RectTransform>().anchoredPosition = originPos;
                originSlot.curItem = detectedItem;
            }

            //감지된 슬롯 위로 아이템을 이동시켜줘.
            this.transform.SetParent(detectedSlot.transform);
            this.GetComponent<RectTransform>().anchoredPosition = originPos;
            detectedSlot.curItem = this;
            originSlot = detectedSlot;
        }
        else //감지된 슬롯이 없다면
        {
            this.GetComponent<RectTransform>().anchoredPosition = originPos;
            originSlot.curItem = this;
        }
    }

    public void OnPointerClick()
    {
        //Debug.Log($"{gameObject.name} item clicked");
        InventoryManager.InvenManager_Instance.SetLastClickedItem(this);
    }
}
