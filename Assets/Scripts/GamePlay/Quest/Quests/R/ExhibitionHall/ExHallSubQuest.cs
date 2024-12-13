using UnityEngine;

public class ExHallSubQuest : Clickable
{
    public GameObject ImagePanel;
    public GameObject NewImage;
    [SerializeField] private InventoryManager invenManager;

    public override void Clicked()
    {
        if (invenManager.LastClickedItem != null) 
        {
            //Debug.Log($"LastClickedItem: {invenManager.LastClickedItem.name}, NewImage Active: {NewImage.activeSelf}");

            if (invenManager.LastClickedItem.name == "Flashlight" && !NewImage.activeSelf)
            {
                NewImage.SetActive(true);
                Debug.Log("NewImage active");
            }

            else if (invenManager.LastClickedItem.name == "RustedSword" && NewImage.activeSelf)
            {
                Debug.Log("New item found! Quest completed.");
            }
        }
    }
}
