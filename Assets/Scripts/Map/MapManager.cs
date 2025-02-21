using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager instance = null;
    private DataManager dataManager;
    private SaveDataClass saveData;

    [SerializeField] private List<NodeClass> nodes;
    public Dictionary<string, NodeClass> nodeMap = new Dictionary<string, NodeClass>();
    public NodeClass cur_node;
    public GameObject playerOnMinimap;
    //추가된 거 edgeMap
    [HideInInspector] public Dictionary<string, List<string>> edgeMap = new Dictionary<string, List<string>>();
    [HideInInspector] public List<List<string>> edges = new List<List<string>>(){
        new List<string> {"R_Lobby_1F", "LawClassroom"}, new List<string> {"R_Lobby_2F", "ExhibitionHall"},
        new List<string> {"R_Lobby_1F", "CafeNamu"}, new List<string> {"LawClassroom", "ExhibitionHall"},
        new List<string> {"R_Lobby_1F", "Playground"}, new List<string> {"R_Lobby_2F", "H_Lobby"},
        new List<string> {"R_Lobby_1F", "R_Lobby_2F"},
        new List<string> {"Playground", "H_Lobby"}, new List<string> {"Playground", "T_Hallway"},
        new List<string> {"H_Lobby", "Healthroom"}, new List<string> {"H_Lobby", "Clubroom"},
        new List<string> {"Clubroom", "CentralLibrary"}, new List<string> {"CentralLibrary", "C_Classroom"},
        new List<string> {"C_Classroom", "CafeForest"}, new List<string> {"C_Classroom", "C_Hallway"}, 
        new List<string> {"C_Hallway", "CafeForest"}, new List<string> {"C_Hallway", "T_Hallway"}, 
        new List<string> {"T_Hallway", "ReadingRoom"}, new List<string> {"T_Hallway", "EngineeringLab"}, 
        new List<string> {"ReadingRoom", "EngineeringLab"}, new List<string> {"C_Hallway", "OldDormitory"},
    };

    
    public void AddEdge(NodeClass from, NodeClass to)
    {
        from.neighbors.Add(to);
        to.neighbors.Add(from);
    }
    public void AddEdgeMap(string from, string to)
    {
        if(!edgeMap.ContainsKey(from)) edgeMap.Add(from, new List<string>());
        if(!edgeMap.ContainsKey(to)) edgeMap.Add(to, new List<string>());
        edgeMap[from].Add(to);
        edgeMap[to].Add(from);
    }
    public void RemoveEdgeMap(string from, string to)
    {
        edgeMap[from].Remove(to);
        edgeMap[to].Remove(from);
    }
    
    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Debug.Log("MapManager duplicate error, destroying duplicate instance.");
            //Destroy(this.gameObject); // �ߺ��� �ν��Ͻ��� �ı�
            //return;
        }

        foreach (var node in nodes)
            nodeMap.Add(node.node_name, node);
        
        NodeClass from, to;
        foreach(var edge in edges)
        {
            from = nodeMap[edge[0]];
            to = nodeMap[edge[1]];
            AddEdge(from, to);
        }

        string _from, _to;
        foreach(var n in edges)
        {
            _from = n[0];
            _to = n[1];
            AddEdgeMap(_from, _to);
        }
    }

    void Start()
    {
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
        cur_node = nodeMap[saveData.cur_position];
        cur_node.gameObject.SetActive(true);
        playerOnMinimap.transform.position = cur_node.posOnMap.position;
    }

    public static MapManager MapManager_Instance
    {
        get
        {
            if (null== instance)
            {
                Debug.Log("MapManager is null");
                return null;
            }
            return instance;
        }
    }

    public List<NodeClass> returnNodes()
    {
        return nodes;
    }

}