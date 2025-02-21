using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance = null;
    [SerializeField] TextAsset csvFile; //Excel csv파일
    //private Dictionary<int, string[]> splitedExcel = new Dictionary<int, string[]>();
    private List<string> systemMessage = new List<string>();
    private List<string> ingameMessage = new List<string>();
    private List<string[]> splitedExcel = new List<string[]>();
    /*
     * excel을 csv로 변환하면 ,로 셀을 구분한다
        셀 하나에 여러줄이 있다면 "여러줄입니다","여러줄입니다"로 구분한다
        셀 하나에 ,가 있다면 "쉼,표"로 구분한다.
        row는 줄바꿈으로 구분한다.
        필요한건 구분, 위치, 퀘스트, 조건/아이템 이름, 내용, 결과/아이템 용도
        B, C, D, E, F, G 열 이 필요하다
    */

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }

        string[] textRows = Regex.Split(csvFile.text, @",\r\n"); //,\r\n을 기준으로 row 분할
        for (int i = 2; i < textRows.Length; i++)
        {
            /*
             * ,                    # 콤마를 찾음
                (?=                 # 조건(Lookahead): 콤마 뒤의 특정 조건을 만족하는 경우만 나눔
                  (?:               # 그룹 시작 (캡처하지 않음)
                    [^\""]*         # 따옴표가 아닌 어떤 문자든 0개 이상
                    \""[^\""]*\""   # "로 시작하고, 따옴표 안의 모든 문자를 포함, 다시 "로 끝남
                  )*                # 위 패턴이 0번 이상 반복될 수 있음
                  [^\""]*$          # 마지막으로 따옴표가 닫히지 않은 문자열
)
             */
            string pattern = @",(?=(?:[^\""]*\""[^\""]*\"")*[^\""]*$)";
            string[] result = Regex.Split(textRows[i], pattern);
            splitedExcel.Add(result);
            /*
             * Dic에 구분 / 위치 / 퀘스트 / 조건,아이템이름 / 내용 / 결과,아이템용도 순으로 
             * 행 마다 저장한다
             */
        }

        /*
        foreach (var row in splitedExcel)
        {
            foreach (var column in row.Value)
            {
                Debug.Log(column.ToString());
            }
            Debug.Log("");
        }
        */
    }
    void Start()
    {
        systemMessage = GetIngameDialogue("시스템 대사", "시스템", "범용");
        ingameMessage = GetIngameDialogue("인게임 대사", "시스템", "범용");
    }

    public static DialogueManager DialoguManager_Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }
    /*
    public string[] GetIngameDialogue(int index)
    {
        if(!splitedExcel.ContainsKey(index - 3))
        {
            Debug.LogError("No dialogue Index");
            return null;
        }
        return splitedExcel[index - 3];
    }*/



    public List<string> GetIngameDialogue(string type, string location, string quest)
    {// 구분, 위치, 퀘스트를 입력하면 해당 행의 내용을 반환
        List<string> result = new List<string>();
        foreach (var row in splitedExcel)
        {
            if (row[1] == type && row[2] == location && row[3] == quest)
            {
                result.Add(row[5]);
            }
        }
        return result;
    }

    public string GetQuestDialogue(string type, string quest)
    {//Todolist 작성을 위한 엑셀 불러오기
        string result = "null";
        foreach (var row in splitedExcel)
        {
            if (row[1] == type && row[3] == quest)
            {
                result = row[5];
                return result;
            }
        }
        return result;
    }

    public List<string> GetItemDiscription(string itemName)
    {
        List<string> result = new List<string>();
        foreach (var row in splitedExcel)
        {
            if (row[7] == itemName)
            {
                result.Add(row[4]);//이름
                result.Add(row[5]);//설명
                return result;
            }
        }
        return null;
    }

    public string GetSystemDialogue(string type, int index)
    {
        if (type == "시스템 대사")
            return systemMessage[index];
        else
            return ingameMessage[index];
    }
}
