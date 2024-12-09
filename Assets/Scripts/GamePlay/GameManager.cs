using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Camera cam;
    private RaycastHit2D hit;
    private Vector3 rayDir = Vector3.forward;
    private Vector3 mousePos;
    public bool isUiOpened = false;

    //관리할 퀘스트와 아이템들
    public List<Quest> Quests = new List<Quest>();
    public List<Item> Items = new List<Item>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }
        //cam = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(mousePos, rayDir * 10, Color.red, 0.5f);
            hit = Physics2D.Raycast(mousePos, rayDir, 10f);
            //Debug.Log(hit.collider.name);
            DetectedFunction();
        }
    }

    public void TurnOffUI()
    {
        isUiOpened = false;
    }

    void DetectedFunction()
    {
        if(hit && hit.collider.GetComponent<Clickable>() && !isUiOpened)
        {
            Clickable obj = hit.collider.GetComponent<Clickable>();
            obj.Clicked();
        }
    }

    public static GameManager GameManager_Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }
}
