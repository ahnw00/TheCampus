using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance = null;
    [SerializeField] TextAsset csvFile; //Excel csvÆÄÀÏ
    private Dictionary<int, string[]> splitedExcel = new Dictionary<int, string[]>();
    /*
     * excelÀ» csv·Î º¯È¯ÇÏ¸é ,·Î ¼¿À» ±¸ºÐÇÑ´Ù
        ¼¿ ÇÏ³ª¿¡ ¿©·¯ÁÙÀÌ ÀÖ´Ù¸é "¿©·¯ÁÙÀÔ´Ï´Ù","¿©·¯ÁÙÀÔ´Ï´Ù"·Î ±¸ºÐÇÑ´Ù
        ¼¿ ÇÏ³ª¿¡ ,°¡ ÀÖ´Ù¸é "½°,Ç¥"·Î ±¸ºÐÇÑ´Ù.
        row´Â ÁÙ¹Ù²ÞÀ¸·Î ±¸ºÐÇÑ´Ù.

        ÇÊ¿äÇÑ°Ç ±¸ºÐ, À§Ä¡, Äù½ºÆ®, Á¶°Ç/¾ÆÀÌÅÛ ÀÌ¸§, ³»¿ë, °á°ú/¾ÆÀÌÅÛ ¿ëµµ
        B, C, D, E, F, G ¿­ ÀÌ ÇÊ¿äÇÏ´Ù
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

        string[] textRows = Regex.Split(csvFile.text, @",\r\n"); //,\r\nÀ» ±âÁØÀ¸·Î row ºÐÇÒ
        for (int i = 2; i < textRows.Length; i++)
        {
            /*
             * ,                    # ÄÞ¸¶¸¦ Ã£À½
                (?=                 # Á¶°Ç(Lookahead): ÄÞ¸¶ µÚÀÇ Æ¯Á¤ Á¶°ÇÀ» ¸¸Á·ÇÏ´Â °æ¿ì¸¸ ³ª´®
                  (?:               # ±×·ì ½ÃÀÛ (Ä¸Ã³ÇÏÁö ¾ÊÀ½)
                    [^\""]*         # µû¿ÈÇ¥°¡ ¾Æ´Ñ ¾î¶² ¹®ÀÚµç 0°³ ÀÌ»ó
                    \""[^\""]*\""   # "·Î ½ÃÀÛÇÏ°í, µû¿ÈÇ¥ ¾ÈÀÇ ¸ðµç ¹®ÀÚ¸¦ Æ÷ÇÔ, ´Ù½Ã "·Î ³¡³²
                  )*                # À§ ÆÐÅÏÀÌ 0¹ø ÀÌ»ó ¹Ýº¹µÉ ¼ö ÀÖÀ½
                  [^\""]*$          # ¸¶Áö¸·À¸·Î µû¿ÈÇ¥°¡ ´ÝÈ÷Áö ¾ÊÀº ¹®ÀÚ¿­
                )
             */
    string pattern = @",(?=(?:[^\""]*\""[^\""]*\"")*[^\""]*$)";
            string[] result = Regex.Split(textRows[i], pattern);
            splitedExcel.Add(i - 2, result);
            /*
             * Dic¿¡ ±¸ºÐ / À§Ä¡ / Äù½ºÆ® / Á¶°Ç,¾ÆÀÌÅÛÀÌ¸§ / ³»¿ë / °á°ú,¾ÆÀÌÅÛ¿ëµµ ¼øÀ¸·Î 
             * Çà ¸¶´Ù ÀúÀåÇÑ´Ù
             */
        }

        foreach (var row in splitedExcel)
        {
            foreach (var column in row.Value)
            {
                //Debug.Log(column.ToString());
            }
            //Debug.Log("");
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
