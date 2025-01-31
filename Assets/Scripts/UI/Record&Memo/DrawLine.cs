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
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            //Vector3 camPos = cam.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 instantiatePos = new Vector3(camPos.x, camPos.y, 0f);
            GameObject obj = Instantiate(linePrefab, curPage.transform);
            lr = obj.GetComponent<LineRenderer>();
            //Vector2 tmp = Input.mousePosition;
            points.Add(Input.mousePosition);
            //points.Add(new Vector2(tmp.x - Screen.width / 2, tmp.y - Screen.height / 2));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);
        }
        else if(Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;
            //Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {
                //points.Add(new Vector2(pos.x - 960f, pos.y - 540f));
                //points.Add(new Vector2(pos.x - Screen.width / 2, pos.y - Screen.height / 2));
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
