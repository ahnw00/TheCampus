using UnityEngine;

public class QuestPanel : Clickable
{
    [SerializeField] GameObject quest;
    public override void Clicked()
    {//����Ʈ �гο� ��Ī�Ǵ� ����Ʈ ���� ����
        quest.SetActive(true);
        QuestManager.QuestManager_instance.OnQuestPanelClicked(quest);
    }
}
