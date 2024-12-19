using UnityEngine;

public class QuestBtn : Clickable
{
    [SerializeField] private GameObject quest;

    public override void Clicked()
    {
        base.Clicked();
        if(flag == 1)
        {
            QuestBtnFunc();
        }
        else Invoke("Delayed", 3.05f);
    }

    void Delayed()
    {
        if(flag == 1)
        {
            QuestBtnFunc();
        }
    }

    void QuestBtnFunc()
    {
        quest.SetActive(true);
        GameManager.GameManager_Instance.isUiOpened = true;
        QuestManager.QuestManager_instance.OnQuestBtnClicked(quest);
    }
}
