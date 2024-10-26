using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject room3;
    [SerializeField] private GameObject room4;
    private List<GameObject> rooms;
    private string cur_room;
    private bool isOnCoroutine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = FindFirstObjectByType<Camera>();
        cur_room = "room1";
        rooms = new List<GameObject>(){room1, room2, room3, room4};
        //
    }

    public void GoLeft()
    {
        if(!isOnCoroutine)
        {
            int idx = rooms.FindIndex(r => r.name == cur_room);
            if(idx == 0)
            {
                GameObject temp = rooms[rooms.Count - 1];
                rooms.RemoveAt(rooms.Count - 1);
                rooms.Insert(0, temp);
                cur_room = rooms[0].name;
                rooms[0].gameObject.transform.position = new Vector2(rooms[1].gameObject.transform.position.x - 19.2f, 0);
            }
            else
            {
                cur_room = rooms[idx - 1].name;
            }
            Debug.Log(cur_room);
            if(idx == 0) StartCoroutine(CamCoroutine(rooms[0]));
            else StartCoroutine(CamCoroutine(rooms[idx - 1]));
        }
    }

    public void GoRight()
    {
        if(!isOnCoroutine)
        {
            int idx = rooms.FindIndex(r => r.name == cur_room);
            if(idx == 3)
            {
                GameObject temp = rooms[0];
                rooms.RemoveAt(0);
                rooms.Add(temp);
                cur_room = rooms[3].name;
                rooms[3].gameObject.transform.position = new Vector2(rooms[2].gameObject.transform.position.x + 19.2f, 0);
            }
            else
            {
                cur_room = rooms[idx + 1].name;
            }
            Debug.Log(cur_room);
            if(idx == 3) StartCoroutine(CamCoroutine(rooms[3]));
            else StartCoroutine(CamCoroutine(rooms[idx + 1]));
        }
    }

    IEnumerator CamCoroutine(GameObject target)
    {
        isOnCoroutine = true;
        Transform cur_pos = cam.gameObject.transform;
        float time = 0;
        while (time < 1f) //time 리밋을 높이면 카메라가 더 천천히 움직여
        {
            time += Time.deltaTime;
            cam.gameObject.transform.position = new Vector3(Mathf.Lerp(cur_pos.position.x, target.transform.position.x, time), 0, -10);
            yield return null;       
        }
        isOnCoroutine = false;
    }
}
