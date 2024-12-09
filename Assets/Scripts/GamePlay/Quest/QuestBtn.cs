using UnityEngine;

public class QuestBtn : Clickable
{
    [SerializeField] private GameObject quest;

    public override void Clicked()
    {
        quest.SetActive(true);
        GameManager.GameManager_Instance.isUiOpened = true;
        QuestManager.QuestManager_instance.OnQuestBtnClicked(quest);
    }
}
