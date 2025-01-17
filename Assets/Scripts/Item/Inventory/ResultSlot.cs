using UnityEngine;

public class ResultSlot : MonoBehaviour
{
    public GameObject curItem = null;

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (curItem == col.GetComponent<ItemClass>() || !curItem)
        {
            curItem = null;
            col.GetComponent<ItemClass>().detectedSlot = null;
        }
    }
}
