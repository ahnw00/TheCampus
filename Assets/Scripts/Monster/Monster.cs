using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Monster : MonoBehaviour 
{
    public static Monster Monster_Instance { get; private set; }
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


        //priority_queue�� ��� ������� ����
        while (priority_queue.Count > 0)
        {
            var (currentDistance, currentNode) = priority_queue.Min;
            priority_queue.Remove(priority_queue.Min);

            //���� ����� �Ÿ��� �̹� �ִ� �Ÿ����� ū ��� �ݺ��� ����
            if (currentDistance > from_start_distance[currentNode])
                continue;

            //���� ����� ������� ��� �湮
            foreach (var edge in mapManager.nodeMap[currentNode].neighbors)
            {
                string neighborNode = edge.node_name;  // �̿� ����� �̸�
                int new_distance = currentDistance + 1;  // ����ġ 1 (�ʿ信 ���� edge�� ����ġ�� ����� �� ����)

                // ���ο� ��ΰ� �� ª���� �Ÿ� ����
                if (new_distance < from_start_distance[neighborNode])
                {
                    // ���� �켱���� ť�� ���� �����ϱ� ���� ���� �� �߰�
                    priority_queue.Remove((from_start_distance[neighborNode], neighborNode));
                    from_start_distance[neighborNode] = new_distance;
                    priority_queue.Add((new_distance, neighborNode));
                }
            }
        }

        return from_start_distance;
    }

    NodeClass getPlayerNode() //�ӽ�
    {
        return current_monster_node;
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
