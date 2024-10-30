using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeClass : MonoBehaviour
{
    private List<NodeClass> _neighbors;
    public string node_name;

    public List<NodeClass> neighbors //capsule _neighbors
    {
        get
        {
            _neighbors = _neighbors ?? new List<NodeClass>();
            return _neighbors;
        }
    }
}