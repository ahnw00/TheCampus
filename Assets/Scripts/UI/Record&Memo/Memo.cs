using UnityEngine;

public class Memo : MonoBehaviour
{
    [SerializeField] private DrawLine drawLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ChangePage(GameObject go)
    {
        drawLine.curPage = go;
    }

    public void SetDrawMode()
    {
        drawLine.enabled = true;
    }

    public void OffDrawMode()
    {
        drawLine.enabled = false;
    }
}
