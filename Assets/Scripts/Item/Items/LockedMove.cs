using System;
using System.Collections;
using UnityEditor.Overlays;
using UnityEngine;

public class LockedMove : Clickable
{
    private DataManager dataManager;
    private SaveDataClass saveData;
    private MapManager mapManager;
    private SoundManager soundManager;
    [SerializeField] private string targetPlace;

    [SerializeField] private GameObject connectedDoor;
    [SerializeField] private string requiredItem;
    [SerializeField] private GameObject vine;
    private bool isOpened = false;

    [SerializeField] private AudioClip vineClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
        mapManager = MapManager.MapManager_Instance;
        soundManager = SoundManager.Instance;
        CheckUnlock();
    }

    private void OnEnable()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        if (PlayerPrefs.HasKey(this.name + "isOpened"))
        {
            isOpened = Convert.ToBoolean(PlayerPrefs.GetInt(this.name + "isOpened"));
            this.GetComponent<SpriteRenderer>().enabled = isOpened;
            vine.SetActive(!isOpened);
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
            soundManager.ChangeSfxClip(vineClip);
            connectedDoor.SetActive(true);
            PlayerPrefs.SetInt(this.name + "isOpened", 1);
            OpenDoor();
            PlayerPrefs.SetInt(connectedDoor.name + "isOpened", 1);
            connectedDoor.GetComponent<LockedMove>().OpenDoor();
            this.GetComponent<SpriteRenderer>().enabled = isOpened;
            vine.SetActive(!isOpened);
            PlayerPrefs.SetInt("Vine", 1);
            PlayerPrefs.Save();
            temp = "녹슨 검으로 덩굴을 베어내니 통로가 드러났다.";
            TextManager.TextManager_Instance.PopUpText(temp);
        }
        else
        {
            temp = "나무 덩굴에 막혀 이동할 수 없다.";
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
        saveData.cur_position = targetPlace;
        dataManager.Save();
        mapManager.cur_node.gameObject.SetActive(false);
        mapManager.cur_node = targetNode;
        targetNode.gameObject.SetActive(true);
        mapManager.StartCoroutine(CameraMove());
    }

    public void OpenDoor()
    {
        isOpened = true;
    }
}
