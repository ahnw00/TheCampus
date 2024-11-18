using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToBtn : Clickable
{
    private MapManager mapManager;
    [SerializeField] private string targetPlace;
    
    void Start()
    {
        mapManager = MapManager.MapManager_Instance;
    }
    public override void Clicked()
    {
        NodeClass targetNode = mapManager.nodeMap[targetPlace];
        //Debug.Log(targetNode.node_name);
        mapManager.cur_node.gameObject.SetActive(false);
        mapManager.cur_node = targetNode;
        targetNode.gameObject.SetActive(true);
        mapManager.StartCoroutine(CameraMove());
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
}
