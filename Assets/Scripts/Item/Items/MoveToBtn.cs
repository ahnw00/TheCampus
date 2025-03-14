using System.Collections;
using UnityEngine;

public class MoveToBtn : Clickable
{
    private MapManager mapManager;
    private DataManager dataManager;
    private SaveDataClass saveData;
    [SerializeField] private string targetPlace;
    
    void Start()
    {
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
        mapManager = MapManager.MapManager_Instance;

        if (PlayerPrefs.HasKey(this.name) && PlayerPrefs.GetInt(this.name) == 1)
            this.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(this.name) && PlayerPrefs.GetInt(this.name) == 1)
            this.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Clicked()
    {
        base.Clicked();
        if (flag == 1)
        {
            MoveFunc();
        }
        else Invoke("Delayed", searchingTime + 0.05f);
    }

    IEnumerator CameraMove()
    {
        Transform cur_pos = mapManager.playerOnMinimap.transform;
        float time = 0;
        while(time < 1f)
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
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void MoveFunc()
    {
        mapManager.PlayMoveAudio();
        NodeClass targetNode = mapManager.nodeMap[targetPlace];
        //Debug.Log(targetNode.node_name);
        saveData.cur_position = targetPlace;
        dataManager.Save();
        mapManager.cur_node.gameObject.SetActive(false);
        mapManager.cur_node = targetNode;
        targetNode.gameObject.SetActive(true);
        mapManager.StartCoroutine(CameraMove());
        LocationAndTodoList.LocationAndTodoList_Instance.SetLocation(MapManager.MapManager_Instance.cur_node.node_name);
    }
}
