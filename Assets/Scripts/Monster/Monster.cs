using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private static NodeClass current_monster_node;
    private static NodeClass current_player_node;
    private static bool isActive = true;
    private static int INF = int.MaxValue;
    private static List<NodeClass> nodes;
    private static Dictionary<(NodeClass, NodeClass), int> from_to_distances;
    private static Dictionary<(NodeClass, NodeClass), NodeClass> next;
    private static List<string> shortest_path;

    private void Start()
    {
        current_monster_node = MapManager.MapManager_Instance.nodeMap["EngineeringLab"];
        current_player_node = MapManager.MapManager_Instance.nodeMap["R_Lobby_1F"];
        MonsterActive();
        FloydWarshallAlgorithm();
        shortest_path = GetPath(current_monster_node, current_player_node, next);
        //foreach(var i in shortest_path)
        //{
        //    Debug.Log(i);
        //}
    }
    public void MonsterActive()
    {
        isActive = true;
        //Debug.Log("monster active");
        

        /* 폐기한 다익스트라
        string current_player_node = getPlayerNode();
        //dijkstra(current_monster_node, current_player_node);
        
        while(current_monster_node != current_player_node && isAcitive)
        {
            string monster_next_node = dijkstra(current_monster_node, current_player_node);
            current_monster_node = monster_next_node;

            if (current_monster_node == current_player_node)
                Debug.Log("game over");
        }
        */
    }

    public void MonsterDeactive()
    {
        isActive = false;
        //Debug.Log("MonsterDeactive");
    }
    
    public static void FloydWarshallAlgorithm()
    {
        nodes = MapManager.MapManager_Instance.returnNodes(); // 모든 노드의 리스트
        from_to_distances = new Dictionary<(NodeClass, NodeClass), int>(); // from, to 노드 간 거리 저장
        next = new Dictionary<(NodeClass, NodeClass), NodeClass>(); // 경로 추적용 배열

        //모든 노드 초기화
        foreach (var from in nodes)
        {
            foreach (var to in nodes)
            {
                if (from == to) // 자기자신이라면
                {
                    from_to_distances[(from, to)] = 0; // 자기 자신으로의 거리
                    next[(from, to)] = null;  // 자기 자신 경로는 필요 없음
                }
                else if (from.neighbors.Contains(to)) // from에 to가 이웃한다면
                {
                    // 이웃한 노드라면 거리 1
                    from_to_distances[(from, to)] = 1;
                    next[(from, to)] = to; // 다음노드는 to
                }
                else
                {
                    // 이웃하지 않으면 거리 무한대
                    from_to_distances[(from, to)] = INF;
                    next[(from, to)] = null;
                }
            }
        }

        //Floyd-whashell argorithm
        foreach (var middle in nodes)
        {
            foreach (var start in nodes)
            {
                foreach (var end in nodes)
                {
                    if (from_to_distances[(start, middle)] < INF && from_to_distances[(middle, end)] < INF)
                    {
                        int new_distance = from_to_distances[(start, middle)] + from_to_distances[(middle, end)];
                        if (new_distance < from_to_distances[(start, end)])
                        {// 새로 계산한 거리가 기존 거리보다 더 짧으면 업데이트
                            from_to_distances[(start, end)] = new_distance;
                            next[(start, end)] = next[(start, middle)]; // 경로업데이트
                        }
                    }
                }
            }
        }
    }

    List<string> GetPath(NodeClass start, NodeClass end, Dictionary<(NodeClass, NodeClass), NodeClass> next)
    {
        var path = new List<string>();
        if (next[(start, end)] == null)
        {
            return path; // 경로가 없으면 빈 리스트 반환
        }

        NodeClass current = start;
        while (current != end)
        {
            path.Add(current.node_name);
            current = next[(current, end)];
        }
        path.Add(end.node_name); // 마지막 노드 추가
        return path;
    }
}

    
        




    /*

        string dijkstra(string from, string to)
    {
        List<string> shortest_path = CalculateShortestPath(from, to);
        return shortest_path[1];
        
    }

    //�ִܰŸ� ��ȯ
    public List<string> CalculateShortestPath(string start_node_name, string end_node_name)
    {
        var from_start_distance = new Dictionary<string, int>(); //start�κ��� ��� ��� ���� �ִ� �Ÿ�
        var previous_node = new Dictionary<string, string>(); // �� ������ ���� ��带 ����
        var priority_queue = new SortedSet<(int distance, string node)>();
        List<string> nodes = MapManager.MapManager_Instance.nodeMap.Keys.ToList(); //graph�� ��� ����Ʈ

        //���� ��带 ������ ��� ��� �ִ밪 �Է�
        foreach (var node in nodes)
        {
            from_start_distance.Add(node, int.MaxValue);
            previous_node[node] = null; // ���� ��带 null�� �ʱ�ȭ
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

                    previous_node[neighborNode] = currentNode;  // �ִ� �Ÿ��� ����, ��带 ���
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
            step = previous_node[step];
        }

        // ���� ���� Ÿ�� ��尡 ������� ���� ��� �� ����Ʈ ��ȯ
        if (path[0] != start_node_name)
            path.Clear();

        printDijkstra(from_start_distance, path);

        return path;
    }
    */
    
    /*
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
    */

