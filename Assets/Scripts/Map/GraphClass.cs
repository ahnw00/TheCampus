using System.Collections.Generic;
using UnityEngine;

public class GraphClass
{
    private List<NodeClass> nodeList;

    public void AddNode(NodeClass node)
    {
        nodeList.Add(node);
    }
    public void AddEdge(NodeClass from, NodeClass to)
    {
        from.neighbors.Add(to);
        to.neighbors.Add(from);
    }
}