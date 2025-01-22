using UnityEngine;

public class InactivedItem : ObtainableItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (!PlayerPrefs.HasKey(itemKey))
        {//고유 ID를 가진 아이템이 월드맵에서 이미 획득되었다면 비활성화
            this.gameObject.SetActive(false);
        }
    }
}
