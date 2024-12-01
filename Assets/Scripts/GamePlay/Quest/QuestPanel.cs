using UnityEngine;

public class QuestPanel : Clickable
{
    [SerializeField] GameObject quest;
    public override void Clicked()
    {//퀘스트 패널에 매칭되는 퀘스트 최초 실행
        quest.SetActive(true);
        QuestManager.QuestManager_instance.OnQuestPanelClicked(quest);
    }
}
