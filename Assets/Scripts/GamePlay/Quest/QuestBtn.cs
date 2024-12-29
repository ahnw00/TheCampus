using UnityEngine;

public class QuestBtn : Clickable
{
    [SerializeField] private GameObject quest; //this에 연결된 퀘스트 패널

    public override void Clicked()
    {//클릭되었을때
        base.Clicked();
        if(flag == 1)
        {//처음이 아닌경우
            QuestBtnFunc();
        }
        else Invoke("Delayed", searchingTime + 0.05f); //처음인경우 게이지표시
    }

    void Delayed()
    {// 처음클릭시 딜레이
        if(flag == 1)
        {
            QuestBtnFunc();
        }
    }

    void QuestBtnFunc()
    {//퀘스트 패널 보여주기
        quest.SetActive(true);
        GameManager.GameManager_Instance.isUiOpened = true; // 퀘스트 패널 열기
        QuestManager.QuestManager_instance.OnQuestBtnClicked(quest); //퀘스트 처음 시작시 실행
    }
}
