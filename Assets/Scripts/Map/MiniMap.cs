using UnityEngine;

public class MiniMap : Clickable
{
    public override void Clicked()
    {
        GameManager.GameManager_Instance.PopOutMiniMap();
    }
}
