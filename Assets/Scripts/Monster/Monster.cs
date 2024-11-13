using System.Collections.Generic;
using System.IO;
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
    string current_monster_node;
    bool isAcitive = true;

    private void Start()
    {
        current_monster_node = "T_Hallway";
        MonsterActive();
    }
    public void MonsterActive()
    {
        isAcitive = true;
        Debug.Log("monster active");
        string current_player_node = getPlayerNode();
        //dijkstra(current_monster_node, current_player_node);
        
        while(current_monster_node != current_player_node && isAcitive)
        {
            string monster_next_node = dijkstra(current_monster_node, current_player_node);
            current_monster_node = monster_next_node;

            if (current_monster_node == current_player_node)
                Debug.Log("game over");
        }
        
    }

    public void MonsterDeactive()
    {
        isAcitive = false;
        Debug.Log("MonsterDeactive");
    }

    string dijkstra(string from, string to)
    {
        List<string> shortest_path = CalculateShortestPath(from, to);
        return shortest_path[1];
        
    }

    //�ִܰŸ� ��ȯ
    public List<string> CalculateShortestPath(string start_node_name, string end_node_name)
    {
        var from_start_distance = new Dictionary<string, int>(); //start�κ��� ��� ��� ���� �ִ� �Ÿ�
        var previousNode = new Dictionary<string, string>(); // �� ������ ���� ��带 ����
        var priority_queue = new SortedSet<(int distance, string node)>();
        List<string> nodes = MapManager.MapManager_Instance.nodeMap.Keys.ToList(); //graph�� ��� ����Ʈ

        //���� ��带 ������ ��� ��� �ִ밪 �Է�
        foreach (var node in nodes)
        {
            from_start_distance.Add(node, int.MaxValue);
            previousNode[node] = null; // ���� ��带 null�� �ʱ�ȭ
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
            foreach (var edge in MapManager.MapManager_Instance.nodeMap[currentNode].neighbors)
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

                    previousNode[neighborNode] = currentNode;  // �ִ� �Ÿ��� ����, ��带 ���
                }
            }
        }

        // �ִ� ��� �籸��
        List <string> path = new List<string>();
        string step = end_node_name;

        // ���� ��忡�� Ÿ�� ������ �Ųٷ� ��θ� ����
        while (step != null)
        {
            path.Insert(0, step); // ����� �պκп� �߰�
            step = previousNode[step];
        }

        // ���� ���� Ÿ�� ��尡 ������� ���� ��� �� ����Ʈ ��ȯ
        if (path[0] != start_node_name)
            path.Clear();

        printDijkstra(from_start_distance, path);

        return path;
    }

    string getPlayerNode() //�ӽ�
    {
        return "R_Lobby";
    }

    void printDijkstra(Dictionary<string, int> dijkstra, List<string> path)
    {
        Debug.Log("print dijkstra");
        foreach(var node in dijkstra) 
        {
            Debug.Log("node : " + node.Key + ", " + "distance : " + node.Value);
        }

        Debug.Log("print path");
        foreach(string node in path)
        {
            Debug.Log("node : " + node);
        }
    }

}
