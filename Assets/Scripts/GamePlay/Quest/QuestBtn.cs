using UnityEngine;

public class QuestBtn : Clickable
{
    [SerializeField] private GameObject quest; // btn�� ����Ű�� quest

    public override void Clicked()
    {//Ŭ���Ǿ�����
        base.Clicked();
        if(flag == 1)
        {//ó�� Ŭ���̶��
            QuestBtnFunc();
        }
        else Invoke("Delayed", searchingTime + 0.05f); //������ �Լ�
    }

    void Delayed()
    {// n�� ������
        if(flag == 1)
        {
            QuestBtnFunc();
        }
    }

    void QuestBtnFunc()
    {//����Ʈ �г� Ȱ��ȭ
        quest.SetActive(true);
        GameManager.GameManager_Instance.isUiOpened = true; // quest �гθ� Ŭ���Ǳ� ����
        QuestManager.QuestManager_instance.OnQuestBtnClicked(quest); // Ŭ������ ������ ����
    }
}
