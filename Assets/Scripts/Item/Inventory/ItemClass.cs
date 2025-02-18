using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClass : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    public ItemSlot detectedSlot; //감지된 슬롯(감지될 때마다 바뀜)
    public ItemSlot originSlot; //이동하기 전에 있었던 슬롯을 저장
    private Vector2 originPos = new Vector2(0, 0);
    [SerializeField] private int weight;
    private AudioSource itemSFX;
    private void Start()
    {
        itemSFX = this.GetComponent<AudioSource>();
    }

    public int ItemWeight()
    {
        return weight;
    }

    public void OnPointerDown()
    {
        originSlot = detectedSlot;
        if(!gameManager)
        {
            gameManager = GameManager.GameManager_Instance;
            cam = gameManager.cam;
        }
    }

    public void OnDrag()
    {
        Vector3 targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.GetComponent<RectTransform>().position = new Vector3(targetPos.x, targetPos.y, 0f);
    }

    public void OnPointerUp() //포인터를 땠을 때
    {
        //좀 아쉬운 부분...
        LawClassroom lc = FindFirstObjectByType<LawClassroom>();
        if((lc && lc.isCoroutineRunning) || !detectedSlot) //천칭이 켜져있거나 감지된 슬롯이 없다면
        {
            this.GetComponent<RectTransform>().anchoredPosition = originPos;
            if (originSlot)
            {
                originSlot.curItem = this;
            }
        }
        else if(detectedSlot) //&& !lc.isCoroutineRunning) //감지된 슬롯이 있다면
        {
            //감지된 슬롯 위에 아이템이 있고 그 아이템이 본인이 아니라면
            if (detectedSlot.curItem && detectedSlot.curItem != this && originSlot)
            {
                //감지된 슬롯 위의 아이템을 현재의 아이템이 있던 슬롯으로 옮겨주는 과정
                ItemClass detectedItem = detectedSlot.curItem; //감지된 슬롯 위의 아이템
                detectedItem.transform.SetParent(originSlot.transform);
                detectedItem.GetComponent<RectTransform>().anchoredPosition = originPos;
                originSlot.curItem = detectedItem;
            }

            if(detectedSlot.GetComponent<ScaleSlot>())
            {
                detectedSlot.GetComponent<ScaleSlot>().StartScaleCoroutine();
            }

            //감지된 슬롯 위로 아이템을 이동시켜줘.
            this.transform.SetParent(detectedSlot.transform);
            this.GetComponent<RectTransform>().anchoredPosition = originPos;
            detectedSlot.curItem = this;

            if (originSlot)
            {//resultSlot이 아닌 나머지
                if (detectedSlot.GetComponent<CraftSlot>() && !originSlot.GetComponent<CraftSlot>())
                {//inventorySlot -> CraftSlot
                    InventoryManager.InvenManager_Instance.CraftItem();
                }
                else if (originSlot.GetComponent<CraftSlot>() && !detectedSlot.GetComponent<CraftSlot>())
                {//craftSlot -> inventorySlot
                    InventoryManager.InvenManager_Instance.CraftItem();
                }
                else if(originSlot.GetComponent<ScaleSlot>() && !detectedSlot.GetComponent<ScaleSlot>())
                {//scaleSlot -> scaleInventorySlot
                    originSlot.GetComponent<ScaleSlot>().StartScaleCoroutine();
                }
            }
            else
            {//resultSlot -> inventorySlot
                InventoryManager.InvenManager_Instance.DestoryIngredients();
            }
            itemSFX.Play();
            originSlot = detectedSlot;
        }
        //else //감지된 슬롯이 없다면
        //{
        //    this.GetComponent<RectTransform>().anchoredPosition = originPos;
        //    if (originSlot)
        //    {
        //        originSlot.curItem = this;
        //    }
        //}
        InventoryManager.InvenManager_Instance.SetLastClickedItem(this);
    }

    public void OnPointerClick()
    {
        //Debug.Log($"{gameObject.name} item clicked");
        InventoryManager.InvenManager_Instance.SetLastClickedItem(this);
    }
}
