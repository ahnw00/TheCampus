using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    // 기본 2줄 h : 135 , 3줄 h : 161 (135 + 26), 4줄 h : 204 (161 + 43) 5줄 h : 248 (204 + 44) 
    // 한글 36자 = 1줄
    public RectTransform TextPreset;  // 조절할 요소
    public Text textElement;       // Content 안의 Text
    public int maxCharacters = 36; // 최대 문자 수 (조건 설정)

    void UpdateContentSize()
    {
        // 텍스트 길이가 조건을 초과했는지 확인
        if (textElement.text.Length > maxCharacters || textElement.text.Contains("\n"))
        {
            // Text의 preferredHeight에 따라 Content 크기 조정
            float preferredHeight = textElement.preferredHeight;
            Vector2 newSize = TextPreset.sizeDelta;
            newSize.y = preferredHeight; // Content의 높이를 텍스트 높이에 맞게 설정
            TextPreset.sizeDelta = newSize;
        }
    }
}
