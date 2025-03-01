using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitionHall : Quest
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject flashLight; //광원
    [SerializeField] GameObject picture; // 일반그림
    [SerializeField] GameObject hiddenPicture; // 숨겨진 그림
    [SerializeField] GameObject nail;
    [SerializeField] GameObject piece4;
    [SerializeField] Sprite lightOffPicture;
    [SerializeField] Sprite lightToPicture;
    [SerializeField] Sprite lightToHiddenPicture;
    [SerializeField] Sprite tearedHiddenPicture;
    private string todoKey = "Rsub3";

    //클릭을 판단하는 flag
    bool canPictureClick = true;
    bool canHiddenPictureClick = true;
    bool isGetPicture = false;
    public override void Start()
    {
        questName = "ExhibitionHall_SubQuest";
    }

    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        InitializeQuest();
        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            questManager.SaveQuestStatus();
            //todo생성
            locationAndTodoList.SetTodo(todoKey, dialogueManager.GetQuestDialogue("퀘스트 대사", todoKey));
        }
        else if (questStatus == QuestStatus.Completed)
        {
            canPictureClick = false;
            canHiddenPictureClick = false;
            isGetPicture = true;
            this.GetComponent<Image>().sprite = tearedHiddenPicture;
        }
        base.ifQuestBtnClicked();
    }

    public override void ifQuestBtnClicked()
    {//quest버튼이 눌렸을 때마다 실행되는 함수. 여기서는 인벤을 불러온다.
        InitializeQuest();
        base.ifQuestBtnClicked();
    }
    protected override void OnQuestCompleted()
    {
        this.questStatus = QuestStatus.Completed;
        questManager.SaveQuestStatus();
        Debug.Log(questName + "clear");
        locationAndTodoList.DeleteTodo(todoKey);
    }

    public void OnPictureClicked()
    {//일반그림 클릭시 불빛 잠깐나오고 사라지는 함수
        if(inventoryManager.GetSelectedItemName() == "Flashlight" && canPictureClick)
        {
            canPictureClick = false;
            canHiddenPictureClick = false;
            this.GetComponent<Image>().sprite = lightToPicture;
            //flashLight.SetActive(true);
            //flashLight.transform.SetParent(picture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
            textManager.PopUpText(dialogue[0]);
            Invoke("TurnOffLight", 1f); //끄기
        }
        else if(inventoryManager.GetSelectedItemName() != "Flashlight" && canPictureClick)
        {
            textManager.PopUpText(dialogueManager.GetSystemDialogue("시스템 대사", 3));
        }
    }
    public void OnHiddenPictureClicked()
    {//숨겨진 그림 클릭시 실행함수
        if (inventoryManager.GetSelectedItemName() == "Flashlight" && canHiddenPictureClick)
        {//손전등을 들고있으면서 클릭했을때
            canHiddenPictureClick = false;
            canPictureClick = false;
            this.GetComponent<Image>().sprite = lightToHiddenPicture;
            textManager.PopUpText(dialogue[1]);
            //flashLight.SetActive(true);
            //flashLight.transform.SetParent(hiddenPicture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
        }
        else if(inventoryManager.GetSelectedItemName() == "RustedSword" && !canHiddenPictureClick && !isGetPicture)
        {//이미 손전등으로 밝힌 상태에서 선택된아이템이 rustedSword일때
            textManager.PopUpText(dialogue[2]);
            isGetPicture = true;
            this.GetComponent<Image>().sprite = tearedHiddenPicture;
            //hiddenPicture.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/temp/tempPicture"); //이미지 바꾸기
            nail.SetActive(true);
            piece4.SetActive(true);
            OnQuestCompleted();
        }
        else if(inventoryManager.GetSelectedItemName() != "Flashlight" && canHiddenPictureClick)
        {
            textManager.PopUpText(dialogueManager.GetSystemDialogue("시스템 대사", 3));
        }
    }
    void TurnOffLight()
    {//광원 deactive
        this.GetComponent<Image>().sprite = lightOffPicture;
        //flashLight.SetActive(false);
        canHiddenPictureClick = true;
    }

    public void GetPiece4()
    {
        PlayerPrefs.SetInt("Piece4", 1);
        PlayerPrefs.Save();
        piece4.SetActive(false);
        PrintMainQuestDialogue();
    }

    protected override void InitializeQuest()
    {
        base.InitializeQuest();
        GetQuestDialogue("인게임 대사", "전시관", "Rsub3");
    }
}
