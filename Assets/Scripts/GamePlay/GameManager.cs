using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera cam;
    private RaycastHit2D hit;
    private Vector3 rayDir = Vector3.forward;
    private Vector3 mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(mousePos, rayDir * 10, Color.red, 0.5f);
            hit = Physics2D.Raycast(mousePos, rayDir, 10f);
            Debug.Log(hit.collider.name);
            DetectedFunction();
        }
    }

    void DetectedFunction()
    {
        if(hit && hit.collider.GetComponent<MoveToBtn>())
        {
            MoveToBtn btn = hit.collider.GetComponent<MoveToBtn>();
            btn.MoveTo();
        }
    }
}
