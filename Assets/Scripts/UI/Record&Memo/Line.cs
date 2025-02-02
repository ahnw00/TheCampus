using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private DrawLine drawLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drawLine = FindAnyObjectByType<DrawLine>();
    }

    public void EraseLine()
    {
        if (drawLine.curMode == DrawLine.Mode.Erase)
        {
            Destroy(this.gameObject);
        }
    }
}
