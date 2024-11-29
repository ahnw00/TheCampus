using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public bool isItemExist = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if(!isItemExist)
        {
            col.GetComponent<ItemClass>().detectedSlot = this;
            isItemExist = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(isItemExist)
        {
            isItemExist = false;
            col.GetComponent<ItemClass>().detectedSlot = null;
        }
    }
}
