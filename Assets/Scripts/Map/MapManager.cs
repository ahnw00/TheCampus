using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<NodeClass> nodes;
    public Dictionary<string, NodeClass> nodeMap = new Dictionary<string, NodeClass>();
    [HideInInspector] public NodeClass cur_node;
    [HideInInspector] public List<List<string>> edges = new List<List<string>>(){
        new List<string> {"A1", "A2"}, new List<string> {"A2", "A3"},
        new List<string> {"A3", "A1"}, new List<string> {"A1", "B1"},
        new List<string> {"B1", "B3"}, new List<string> {"B3", "B2"},
        new List<string> {"B2", "C1"}, new List<string> {"C1", "C2"},
        new List<string> {"C2", "C3"}
    };

    public void AddEdge(NodeClass from, NodeClass to)
    {
        from.neighbors.Add(to);
        to.neighbors.Add(from);
    }
    
    void Awake() 
    {
        foreach(var node in nodes)
            nodeMap.Add(node.node_name, node);

        NodeClass from, to;
        foreach(var edge in edges)
        {
            from = nodeMap[edge[0]];
            to = nodeMap[edge[1]];
            AddEdge(from, to);
        }
    }

    void Start()
    {
        cur_node = nodeMap["A1"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}