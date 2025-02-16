using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    [SerializeField] private DrawLine drawLine;
    public List<Transform> memoTabs;

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
        drawLine.curMode = DrawLine.Mode.Draw;
    }

    public void SetEraseMode()
    {
        drawLine.curMode = DrawLine.Mode.Erase;
    }

    public void DeleteAllLines()
    {
        foreach(Transform child in drawLine.curPage.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
