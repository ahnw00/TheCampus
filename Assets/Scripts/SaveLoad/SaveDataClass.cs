using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveDataClass
{
    //이 클래스에다가 정보들을 저장할거임.
    //string cur_position;
    public List<string> itemList;
    public bool isMapObtained;

    public SaveDataClass()
    {
        //cur_position = "T_Lobby";
        isMapObtained = false;
        itemList = new List<string>();
        itemList.Add("RustedSword");
    }
}
