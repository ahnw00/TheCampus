using System;
using System.Collections;
using UnityEngine;

public class LockedMove : Clickable
{
    private MapManager mapManager;
    [SerializeField] private string targetPlace;

    [SerializeField] private GameObject connectedDoor;
    [SerializeField] private string requiredItem;
    private bool isOpened = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapManager = MapManager.MapManager_Instance;
        if (PlayerPrefs.HasKey(this.name + "isOpened"))
        {
            isOpened = Convert.ToBoolean(PlayerPrefs.GetInt(this.name + "isOpened"));
            PlayerPrefs.SetInt(this.name, 1);
            flag = 1;
        }
    }

    public override void Clicked()
    {
        base.Clicked();
        if (flag == 1 && isOpened)
            MoveFunc();
        else if (flag == 1 && !isOpened)
            CheckMove();
        else Invoke("Delayed", searchingTime + 0.05f);
    }

    private void CheckMove()
    {
        InventoryManager inven = InventoryManager.InvenManager_Instance;
        string temp;
        if (inven.GetSelectedItemName() == requiredItem)
        {
            isOpened = true;
            connectedDoor.SetActive(true);
            PlayerPrefs.SetInt(this.name + "isOpened", 1);
            PlayerPrefs.SetInt(connectedDoor.name + "isOpened", 1);
            PlayerPrefs.Save();
            temp = "Door opened";
            TextManager.TextManager_Instance.PopUpText(temp);
        }
        else
        {
            temp = "Cannot go";
            TextManager.TextManager_Instance.PopUpText(temp);
        }
    }

    IEnumerator CameraMove()
    {
        Transform cur_pos = mapManager.playerOnMinimap.transform;
        float time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime;
            cur_pos.position = Vector3.Lerp(cur_pos.position, mapManager.cur_node.posOnMap.position, time);
            yield return null;
        }
    }

    void Delayed()
    {
        if (flag == 1)
        {
            CheckMove();
        }
    }

    void MoveFunc()
    {
        NodeClass targetNode = mapManager.nodeMap[targetPlace];
        //Debug.Log(targetNode.node_name);
        mapManager.cur_node.gameObject.SetActive(false);
        mapManager.cur_node = targetNode;
        targetNode.gameObject.SetActive(true);
        mapManager.StartCoroutine(CameraMove());
    }
}
