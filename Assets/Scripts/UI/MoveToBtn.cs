using UnityEngine;

public class MoveToBtn : MonoBehaviour
{
    private MapManager mapManager;
    [SerializeField] private string targetPlace;
    
    void Start()
    {
        mapManager = FindAnyObjectByType<MapManager>();
    }
    public void MoveTo()
    {
        NodeClass targetNode = mapManager.nodeMap[targetPlace];
        mapManager.cur_node.gameObject.SetActive(false);
        mapManager.cur_node = targetNode;
        targetNode.gameObject.SetActive(true);
    }
}
