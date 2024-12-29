using UnityEngine;

public class QuestBtn : Clickable
{
    [SerializeField] private GameObject quest; //this�� ����� ����Ʈ �г�

    public override void Clicked()
    {//Ŭ���Ǿ�����
        base.Clicked();
        if(flag == 1)
        {//ó���� �ƴѰ��
            QuestBtnFunc();
        }
        else Invoke("Delayed", searchingTime + 0.05f); //ó���ΰ�� ������ǥ��
    }

    void Delayed()
    {// ó��Ŭ���� ������
        if(flag == 1)
        {
            QuestBtnFunc();
        }
    }

    void QuestBtnFunc()
    {//����Ʈ �г� �����ֱ�
        quest.SetActive(true);
        GameManager.GameManager_Instance.isUiOpened = true; // ����Ʈ �г� ����
        QuestManager.QuestManager_instance.OnQuestBtnClicked(quest); //����Ʈ ó�� ���۽� ����
    }
}
