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
        string[] textRows = Regex.Split(csvFile.text, @",\r\n"); //,\r\n�� �������� row ����
        for (int i = 2; i < textRows.Length; i++)
        {
            /*
             * ,                    # �޸��� ã��
                (?=                 # ����(Lookahead): �޸� ���� Ư�� ������ �����ϴ� ��츸 ����
                  (?:               # �׷� ���� (ĸó���� ����)
                    [^\""]*         # ����ǥ�� �ƴ� � ���ڵ� 0�� �̻�
                    \""[^\""]*\""   # "�� �����ϰ�, ����ǥ ���� ��� ���ڸ� ����, �ٽ� "�� ����
                  )*                # �� ������ 0�� �̻� �ݺ��� �� ����
                  [^\""]*$          # ���������� ����ǥ�� ������ ���� ���ڿ�
)
             */
            string pattern = @",(?=(?:[^\""]*\""[^\""]*\"")*[^\""]*$)";
            string[] result = Regex.Split(textRows[i], pattern);
            splitedExcel.Add(i - 2, result);
            /*
             * Dic�� ���� / ��ġ / ����Ʈ / ����,�������̸� / ���� / ���,�����ۿ뵵 ������ 
             * �� ���� �����Ѵ�
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
