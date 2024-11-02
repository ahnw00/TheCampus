using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Monster : MonoBehaviour 
{
    /* 
    ������ Ư�� ����Ʈ�� Ŭ���� ���� �� ������ �÷��̾ �Ѿƿ´�.
    Ż�� ����Ʈ �Ǵ� ���� ������ ���� �� ������ ����� ó��
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
        var from_start_distance = new Dictionary<string, int>(); //start�κ��� ��� ��� ���� �ִ� �Ÿ�
        var priority_queue = new SortedSet<(int distance, string node)>();
        List<string> nodes = mapManager.nodeMap.Keys.ToList(); //graph�� ��� ����Ʈ

        //���� ��带 ������ ��� ��� �ִ밪 �Է�
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

    NodeClass getPlayerNode() //�ӽ�
    {
        return current_monster_node;
    }
    
}
