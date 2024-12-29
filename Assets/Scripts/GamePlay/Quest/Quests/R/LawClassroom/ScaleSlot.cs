using UnityEngine;

public class ScaleSlot : ItemSlot
{
    [SerializeField] private Scale parentScale;
    //[SerializeField] private LawClassroom lawClassroom;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        parentScale.sum += col.GetComponent<ItemClass>().weight;
    }

    protected override void OnTriggerExit2D(Collider2D col)
    {
        base.OnTriggerExit2D(col);
        parentScale.sum -= col.GetComponent<ItemClass>().weight;
    }

    public void StartScaleCoroutine()
    {
        parentScale.lawClassroom.StopAllCoroutines();
        StartCoroutine(parentScale.lawClassroom.ScaleCoroutine());
    }    
}
