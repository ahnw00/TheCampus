using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    LineRenderer lr;
    List<Vector2> points = new List<Vector2>();
    public GameObject curPage;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameManager.GameManager_Instance.cam;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            GameObject obj = Instantiate(linePrefab, curPage.transform);
            lr = obj.GetComponent<LineRenderer>();
            points.Add(pos);
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);
        }
        else if(Input.GetMouseButton(0))
        {
            if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }
    }
}
