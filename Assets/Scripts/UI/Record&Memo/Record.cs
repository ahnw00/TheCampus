using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    // �⺻ 2�� h : 135 , 3�� h : 161 (135 + 26), 4�� h : 204 (161 + 43) 5�� h : 248 (204 + 44) 
    // �ѱ� 36�� = 1��
    public RectTransform TextPreset;  // ������ ���
    public Text textElement;       // Content ���� Text
    public int maxCharacters = 36; // �ִ� ���� �� (���� ����)

    void UpdateContentSize()
    {
        // �ؽ�Ʈ ���̰� ������ �ʰ��ߴ��� Ȯ��
        if (textElement.text.Length > maxCharacters || textElement.text.Contains("\n"))
        {
            // Text�� preferredHeight�� ���� Content ũ�� ����
            float preferredHeight = textElement.preferredHeight;
            Vector2 newSize = TextPreset.sizeDelta;
            newSize.y = preferredHeight; // Content�� ���̸� �ؽ�Ʈ ���̿� �°� ����
            TextPreset.sizeDelta = newSize;
        }
    }
}
