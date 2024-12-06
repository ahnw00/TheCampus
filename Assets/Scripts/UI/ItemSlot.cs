using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public ItemClass curItem = null; //현재 슬롯 위에 있는 아이템

    private void Start()
    {
        if(this.transform.childCount != 0)
        {
            curItem = this.transform.GetChild(0).GetComponent<ItemClass>();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<ItemClass>().detectedSlot = this;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(curItem == col.GetComponent<ItemClass>() || !curItem)
        {
            curItem = null;
            col.GetComponent<ItemClass>().detectedSlot = null;
        }
    }
}
