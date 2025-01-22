using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance = null;
    [SerializeField] TextAsset csvFile; //Excel csv����
    private Dictionary<int, string[]> splitedExcel = new Dictionary<int, string[]>();
    /*
     * excel�� csv�� ��ȯ�ϸ� ,�� ���� �����Ѵ�
        �� �ϳ��� �������� �ִٸ� "�������Դϴ�","�������Դϴ�"�� �����Ѵ�
        �� �ϳ��� ,�� �ִٸ� "��,ǥ"�� �����Ѵ�.
        row�� �ٹٲ����� �����Ѵ�.

        �ʿ��Ѱ� ����, ��ġ, ����Ʈ, ����/������ �̸�, ����, ���/������ �뵵
        B, C, D, E, F, G �� �� �ʿ��ϴ�
    */
    void Start()
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

    public static DialogueManager ConvertExcel_Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }
}
