using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineDataCollection
{
    //하나의 LineDataCollection이 여러개의 line들을 가지고 있어
    public List<LineData> lines = new List<LineData>();
}
