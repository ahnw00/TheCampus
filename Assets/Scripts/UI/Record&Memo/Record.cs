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
    // �⺻ 2�� h : 135 , 3�� h : 161 (135 + 26), 4�� h : 204 (161 + 43) 5�� h : 248 (204 + 44) 
    // �ѱ� 36�� = 1��
    // textFormat
    [SerializeField] GameObject subHeadPrefab;
    [SerializeField] GameObject radioPrefab;
    [SerializeField] GameObject speakerPrefab;

    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Transform content; //scroll view content
    GameObject newTextFormat = null; // clone of text format parent
    GameObject textObject = null; // actual text obj
    TextMeshProUGUI textComponent = null; // actual text

    public RectTransform textPreset;  // ������ ���
    public TextMeshProUGUI textElement;       // Content ���� Text
    public int maxCharacters_30Size_InLine = 36; // �ִ� ���� ��
    /*
     ����ص� prefab���ĵ��� �ҷ��ͼ� 
    Scroll View�� Content�� �ڽ����� �����Ͽ�
    json�� ����Ǿ��ִ� ��ũ��Ʈ ������ �� Ÿ�Կ� �°� �ؽ�Ʈ�� ����

    prefab
    - subHeading
    - Radio
    - Speaker
    texst
     */

    private void Start()
    {
        //�׽�Ʈ��
        AddText(TextType.SubHeading, "������ �׽�Ʈ");
        AddText(TextType.Radio, "���� �׽�Ʈ");
        AddText(TextType.Speaker, "�׽�Ʈ", "����");
        AddText(TextType.SubHeading, "������ �׽�Ʈ");
        AddText(TextType.Radio, "���� �׽�Ʈ");
        AddText(TextType.Speaker, "�׽�Ʈ", "����");
        AddText(TextType.SubHeading, "������ �׽�Ʈ");
        AddText(TextType.Radio, "���� �׽�Ʈ");
        AddText(TextType.Speaker, "�׽�Ʈ", "����");
    }

    public void AddText(TextType type, string description, string speaker = null)
    {// json���� type/description/speaker�� ����Ǿ��ִ�. description�� body, speaker�� Ÿ���� speaker �϶��� ���
        switch (type)
        {
            case TextType.SubHeading:
                //�� ��ü�� �ؽ�Ʈ ������Ʈ��
                newTextFormat = Instantiate(subHeadPrefab, content);
                textObject = newTextFormat;
                textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = description;
                break;
            case TextType.Radio:
                //�̹����� �̹� �ִ�. body�� �Է�
                newTextFormat = Instantiate(radioPrefab, content);
                textObject = newTextFormat;
                textObject = textObject.transform.GetChild(1).gameObject;
                textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = description;
                break;
            case TextType.Speaker:
                //ȭ�� �Է�
                newTextFormat = Instantiate(speakerPrefab, content);
                textObject = newTextFormat.transform.GetChild(0).gameObject;
                textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = speaker;
                //body �Է�
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
    ���� �ؽ�Ʈ �з��� ���� ���̸� �ʰ��Ҷ� prefab���� �ҷ��� textObject�� ���̸� ������ �ڿ������� ����� �Լ� 
    */
    void UpdateContentSize(TextType type, string txt)
    {
        // �ؽ�Ʈ ���̰� ������ �ʰ��ߴ��� Ȯ��
        if (txt.Length > maxCharacters_30Size_InLine || txt.Contains("\n"))
        {
            // Text�� preferredHeight�� ���� Content ũ�� ����
            float preferredHeight = textElement.preferredHeight;
            Vector2 newSize = textPreset.sizeDelta;
            newSize.y = preferredHeight; // Content�� ���̸� �ؽ�Ʈ ���̿� �°� ����
            textPreset.sizeDelta = newSize;
        }
    }
}
