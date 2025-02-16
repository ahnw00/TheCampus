using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct SerializableVector2
{
    public float x, y;

    public SerializableVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    // **Vector2를 SerializableVector2로 변환**
    public static SerializableVector2 FromVector2(Vector2 vector)
    {
        return new SerializableVector2(vector.x, vector.y);
    }

    // **SerializableVector2를 Vector2로 변환**
    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
}

[System.Serializable]
public class SaveDataClass
{
    //이 클래스에다가 정보들을 저장할거임.
    //string cur_position;
    public List<string> itemList;
    public List<string> cabinetList;
    public bool isMapObtained;
    public List<string> questList;
    public List<List<List<SerializableVector2>>> memoList;
    public bool test;

    public SaveDataClass()
    {
        //cur_position = "T_Lobby";
        isMapObtained = false;
        itemList = new List<string>();
        cabinetList = new List<string>();
        questList = new List<string>();
        memoList = new List<List<List<SerializableVector2>>>();
        test = false;

        for (int i = 0; i < 3; i++)
        {
            memoList.Add(new List<List<SerializableVector2>>());
        }
    }
}
