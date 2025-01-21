using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class ConvertExcel : MonoBehaviour
{
    [SerializeField] TextAsset csvFile;
    private Dictionary<int, string[]> splitedExcel = new Dictionary<int, string[]>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            splitedExcel.Add(i - 2, result);
            /*
             * Dic에 구분 / 위치 / 퀘스트 / 조건,아이템이름 / 내용 / 결과,아이템용도 순으로 
             * 행 마다 저장한다
             */
        }

        foreach (var row in splitedExcel)
        {
            foreach (var column in row.Value)
            {
                Debug.Log(column.ToString());
            }
            Debug.Log("");
        }

    }
}
