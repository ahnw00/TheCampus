using UnityEngine;

public class QuestBtn : Clickable
{
    [SerializeField] private GameObject quest; // btn이 가리키는 quest

    public override void Clicked()
    {//클릭되었을때
        base.Clicked();
        if(flag == 1)
        {//처음 클릭이라면
            QuestBtnFunc();
        }
        else Invoke("Delayed", searchingTime + 0.05f); //딜레이 함수
    }

    void Delayed()
    {// n초 딜레이
        if(flag == 1)
        {
            QuestBtnFunc();
        }
    }

    void QuestBtnFunc()
    {//퀘스트 패널 활성화
        quest.SetActive(true);
        GameManager.GameManager_Instance.isUiOpened = true; // quest 패널만 클릭되기 위함
        QuestManager.QuestManager_instance.OnQuestBtnClicked(quest); // 클릭마다 실행을 위함
    }
}
