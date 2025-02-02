using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DrawLine : MonoBehaviour
{
    private Memo memo;
    [SerializeField] private GameObject linePrefab;
    LineRenderer lr;
    EdgeCollider2D edgeCol;
    List<Vector2> points = new List<Vector2>();
    public GameObject curPage;
    private Camera cam;
    private float leftX, leftY, rightX, rightY;
    public Mode curMode;

    Vector3 mousePos;
    private Vector3 rayDir = Vector3.forward;
    private RaycastHit2D hit;

    public enum Mode
    {
        Draw,
        Erase
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        memo = this.GetComponent<Memo>();
        cam = GameManager.GameManager_Instance.cam;
        curMode = Mode.Draw;

        float midX = Screen.width / 2;
        float midY = Screen.height / 2;
        midX += curPage.GetComponent<RectTransform>().anchoredPosition.x;
        midY += curPage.GetComponent<RectTransform>().anchoredPosition.y;
        leftX = midX - 649f; leftY = midY - 362f;
        rightX = midX + 581f; rightY = midY + 362f;
    }

    // Update is called once per frame
    void Update()
    {
        if(curMode == Mode.Draw)
        { 
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)) //&& CheckMousePos(Input.mousePosition))
            {
                Debug.Log(Input.mousePosition);
                GameObject obj = Instantiate(linePrefab, curPage.transform);
                lr = obj.GetComponent<LineRenderer>();
                edgeCol = obj.GetComponent<EdgeCollider2D>();
                points.Add(pos);
                lr.positionCount = 1;
                lr.SetPosition(0, points[0]);
            }
            else if(Input.GetMouseButton(0) && CheckMousePos(Input.mousePosition))
            {
                if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
                {
                    points.Add(pos);
                    lr.positionCount++;
                    lr.SetPosition(lr.positionCount - 1, pos);
                    edgeCol.points = points.ToArray();
                }
            }
            else if(Input.GetMouseButtonUp(0))
            {
                points.Clear();
            }
        }
    }

    bool CheckMousePos(Vector2 mousePos)
    {
        if (mousePos.x >= leftX && mousePos.x <= rightX && 
            mousePos.y >= leftY && mousePos.y <= rightY)
            return true;
        return false;
    }
}
