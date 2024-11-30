using UnityEngine;

public class ImageClick : Clickable
{
    public GameObject ExHallPanel;
    public GameObject ExHallBG;

    public override void Clicked()
    {
        Debug.Log($"{gameObject.name} clicked");
        ExHallPanel.SetActive(true);
        ExHallBG.SetActive(true);
    }
}
