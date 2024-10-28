using System.Collections.Generic;
using UnityEngine;

public class NodeClass : MonoBehaviour
{
    private List<NodeClass> _neighbors;

    public List<NodeClass> neighbors //capsule _neighbors
    {
        get
        {
            _neighbors = _neighbors ?? new List<NodeClass>();
            return _neighbors;
        }
    }
}