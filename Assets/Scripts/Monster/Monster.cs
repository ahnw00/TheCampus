using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Monster : MonoBehaviour 
{
    /* 
    괴물은 특정 퀘스트를 클리어 했을 때 등장해 플레이어를 쫓아온다.
    탈출 퀘스트 또는 숨기 동작을 했을 때 괴물은 사라짐 처리
    */
    MapManager mapManager = MapManager.MapManager_Instance;
    NodeClass current_monster_node;

    private void Start()
    {
        
    }
    public void MonsterActive()
    {
        NodeClass current_player_node = getPlayerNode();
        while(current_monster_node.node_name == current_player_node.node_name)
        {
            dijkstra(current_monster_node, current_player_node);

        }
    }

    public void MonsterDeactive()
    {

    }

    void dijkstra(NodeClass monster, NodeClass player)
    {

    }
    public Dictionary<string, int> CalculateShortestPath(string start_node_name)
    {
        var from_start_distance = new Dictionary<string, int>(); //start로부터 모든 노드 사이 최단 거리
        var priority_queue = new SortedSet<(int distance, string node)>();
        List<string> nodes = mapManager.nodeMap.Keys.ToList(); //graph의 노드 리스트

        //시작 노드를 제외한 모든 노드 최대값 입력
        foreach (var node in nodes)
        {
            from_start_distance.Add(node, int.MaxValue);
        }
        from_start_distance[start_node_name] = 0;

        priority_queue.Add((0, start_node_name));

        while (priority_queue.Count > 0)
        {
            var (currentDistance, currentNode) = priority_queue.Min;
            priority_queue.Remove(priority_queue.Min);

            if (currentDistance > from_start_distance[currentNode])
                continue;


            foreach (var edge in mapManager.nodeMap[currentNode].neighbors)
            {


                /*
                int newDist = currentDistance + edge.Weight;
                if (newDist < from_start_distance[edge.To.Name])
                {
                    priority_queue.Remove((from_start_distance[edge.To.Name], edge.To));
                    from_start_distance[edge.To.Name] = newDist;
                    priority_queue.Add((newDist, edge.To));
                }
                */
            }
        }

        return from_start_distance;
    }

    NodeClass getPlayerNode() //임시
    {
        return current_monster_node;
    }
    
}
