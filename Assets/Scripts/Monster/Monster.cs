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
    괴물은 특정 퀘스트를 클리어 했을 때 등장해 플레이어를 쫓아온다.
    탈출 퀘스트 또는 숨기 동작을 했을 때 괴물은 사라짐 처리
    */
    string current_monster_node;
    bool isAcitive = true;

    private void Start()
    {
        current_monster_node = "C3";
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

    //최단거리 반환
    public List<string> CalculateShortestPath(string start_node_name, string end_node_name)
    {
        var from_start_distance = new Dictionary<string, int>(); //start로부터 모든 노드 사이 최단 거리
        var previousNode = new Dictionary<string, string>(); // 각 노드로의 이전 노드를 저장
        var priority_queue = new SortedSet<(int distance, string node)>();
        List<string> nodes = MapManager.MapManager_Instance.nodeMap.Keys.ToList(); //graph의 노드 리스트

        //시작 노드를 제외한 모든 노드 최대값 입력
        foreach (var node in nodes)
        {
            from_start_distance.Add(node, int.MaxValue);
            previousNode[node] = null; // 이전 노드를 null로 초기화
        }
        from_start_distance[start_node_name] = 0;

        priority_queue.Add((0, start_node_name));


        //priority_queue가 모두 비워지면 종료
        while (priority_queue.Count > 0)
        {
            var (currentDistance, currentNode) = priority_queue.Min;
            priority_queue.Remove(priority_queue.Min);

            //현재 노드의 거리가 이미 최단 거리보다 큰 경우 반복문 무시
            if (currentDistance > from_start_distance[currentNode])
                continue;

            //현재 노드의 인접노드 모두 방문
            foreach (var edge in MapManager.MapManager_Instance.nodeMap[currentNode].neighbors)
            {
                string neighborNode = edge.node_name;  // 이웃 노드의 이름
                int new_distance = currentDistance + 1;  // 가중치 1 (필요에 따라 edge의 가중치를 사용할 수 있음)

                // 새로운 경로가 더 짧으면 거리 갱신
                if (new_distance < from_start_distance[neighborNode])
                {
                    // 기존 우선순위 큐의 값을 갱신하기 위해 제거 후 추가
                    priority_queue.Remove((from_start_distance[neighborNode], neighborNode));
                    from_start_distance[neighborNode] = new_distance;
                    priority_queue.Add((new_distance, neighborNode));

                    previousNode[neighborNode] = currentNode;  // 최단 거리로 갱신, 노드를 기록
                }
            }
        }

        // 최단 경로 재구성
        List <string> path = new List<string>();
        string step = end_node_name;

        // 시작 노드에서 타겟 노드까지 거꾸로 경로를 추적
        while (step != null)
        {
            path.Insert(0, step); // 경로의 앞부분에 추가
            step = previousNode[step];
        }

        // 시작 노드와 타겟 노드가 연결되지 않은 경우 빈 리스트 반환
        if (path[0] != start_node_name)
            path.Clear();

        printDijkstra(from_start_distance, path);

        return path;
    }

    string getPlayerNode() //임시
    {
        return "A1";
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
