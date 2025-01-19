using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TextType
{
    SubHeading,
    Radio,
    Speaker
}
public class Record : MonoBehaviour
{
    // 기본 2줄 h : 135 , 3줄 h : 161 (135 + 26), 4줄 h : 204 (161 + 43) 5줄 h : 248 (204 + 44) 
    // 한글 36자 = 1줄
    // textFormat
    [SerializeField] GameObject subHeadPrefab;
    [SerializeField] GameObject radioPrefab;
    [SerializeField] GameObject speakerPrefab;

    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Transform content; //scroll view content
    GameObject newTextFormat = null; // clone of text format parent
    GameObject textObject = null; // actual text obj
    TextMeshProUGUI textComponent = null; // actual text

    public RectTransform textPreset;  // 조절할 요소
    public TextMeshProUGUI textElement;       // Content 안의 Text
    public int maxCharacters_30Size_InLine = 36; // 최대 문자 수
    /*
     등록해둔 prefab형식들을 불러와서 
    Scroll View의 Content의 자식으로 생성하여
    json에 저장되어있는 스크립트 대사들을 각 타입에 맞게 텍스트를 삽입

    prefab
    - subHeading
    - Radio
    - Speaker
    texst
     */

    private void Start()
    {
        //테스트용
        AddText(TextType.SubHeading, "소제목 테스트");
        AddText(TextType.Radio, "라디오 테스트");
        AddText(TextType.Speaker, "테스트", "독백");
        AddText(TextType.SubHeading, "소제목 테스트");
        AddText(TextType.Radio, "라디오 테스트");
        AddText(TextType.Speaker, "테스트", "독백");
        AddText(TextType.SubHeading, "소제목 테스트");
        AddText(TextType.Radio, "라디오 테스트");
        AddText(TextType.Speaker, "테스트", "독백");
    }

    public void AddText(TextType type, string description, string speaker = null)
    {// json에는 type/description/speaker로 저장되어있다. description은 body, speaker는 타입이 speaker 일때만 사용
        switch (type)
        {
            case TextType.SubHeading:
                //그 자체로 텍스트 오브젝트임
                newTextFormat = Instantiate(subHeadPrefab, content);
                textObject = newTextFormat;
                textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = description;
                break;
            case TextType.Radio:
                //이미지는 이미 있다. body만 입력
                newTextFormat = Instantiate(radioPrefab, content);
                textObject = newTextFormat;
                textObject = textObject.transform.GetChild(1).gameObject;
                textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = description;
                break;
            case TextType.Speaker:
                //화자 입력
                newTextFormat = Instantiate(speakerPrefab, content);
                textObject = newTextFormat.transform.GetChild(0).gameObject;
                textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = speaker;
                //body 입력
                textObject = newTextFormat.transform.GetChild(1).gameObject;
                textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = description;
                break;
        }

        //UpdateContentSize(type, description);
        //Canvas.ForceUpdateCanvases();
        scrollbar.value = 0f;
    }
    /*
    만약 텍스트 분량이 많아 길이를 초과할때 prefab으로 불러온 textObject의 길이를 조절해 자연스럽게 만드는 함수 
    */
    void UpdateContentSize(TextType type, string txt)
    {
        // 텍스트 길이가 조건을 초과했는지 확인
        if (txt.Length > maxCharacters_30Size_InLine || txt.Contains("\n"))
        {
            // Text의 preferredHeight에 따라 Content 크기 조정
            float preferredHeight = textElement.preferredHeight;
            Vector2 newSize = textPreset.sizeDelta;
            newSize.y = preferredHeight; // Content의 높이를 텍스트 높이에 맞게 설정
            textPreset.sizeDelta = newSize;
        }
    }
}
