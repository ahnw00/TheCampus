using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GraphClass graph = new GraphClass();
    [SerializeField] private List<NodeClass> nodes = new List<NodeClass>();
    //{a1, a2, a3, b1, b2, b3, c1, c2, c3}
    //이렇게 선언해주면 GraphClass의 nodeList는 의미가 없어지는거 아닌가?
    //그럼 캡슐화는...?
    [HideInInspector] public NodeClass cur_node;
    
    void Awake() 
    {
        foreach(var node in nodes)
            graph.AddNode(node);

        //edge는 노가다로 해줘야하나...?
        //더 좋은 방법 없을까...
        graph.AddEdge(nodes[0], nodes[1]);
        graph.AddEdge(nodes[1], nodes[2]);
        graph.AddEdge(nodes[2], nodes[0]);
        graph.AddEdge(nodes[0], nodes[3]);
        graph.AddEdge(nodes[3], nodes[4]);
        graph.AddEdge(nodes[4], nodes[5]);
        graph.AddEdge(nodes[5], nodes[6]);
        graph.AddEdge(nodes[6], nodes[7]);
        graph.AddEdge(nodes[7], nodes[8]);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}