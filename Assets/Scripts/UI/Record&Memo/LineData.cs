using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineData
{
    public List<Vector2> positions;

    public LineData(List<Vector2> pos)
    {
        positions = pos;
    }
}
