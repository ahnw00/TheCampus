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
        if (this.name == "ToH_Lobby")
        {
            if (PlayerPrefs.HasKey(this.name))
            {
                this.gameObject.SetActive(true);
                isOpened = true;
            }
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
