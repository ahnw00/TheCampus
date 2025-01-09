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

    //클릭을 판단하는 flag
    bool pictureFlag = true;
    bool hiddenPictureFlag = true;
    bool isGetPicture = false;
    public override void Start()
    {
        questName = "ExhibitionHall_SubQuest";
    }

    public override void StartQuest()
    {//퀘스트가 시작할때 실행
        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            inventoryManager = InventoryManager.InvenManager_Instance;
        }
        inventoryManager.SetItemsOnInven(slotList);
    }

    public override void ifQuestBtnClicked()
    {//quest버튼이 눌렸을 때마다 실행되는 함수. 여기서는 인벤을 불러온다.
        base.ifQuestBtnClicked();
    }
    protected override void OnQuestCompleted()
    {
        this.questStatus = QuestStatus.Completed;
        Debug.Log(questName + "clear");
    }

    public void OnPictureClicked()
    {//일반그림 클릭시 불빛 잠깐나오고 사라지는 함수
        if(inventoryManager.GetSelectedItemName() == "Flashlight" && pictureFlag)
        {
            pictureFlag = false;
            flashLight.SetActive(true);
            flashLight.transform.SetParent(picture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
            Invoke("TurnOffLight", 1f); //끄기
        }
    }
    public void OnHiddenPictureClicked()
    {//숨겨진 그림 클릭시 실행함수
        if (inventoryManager.GetSelectedItemName() == "Flashlight" && hiddenPictureFlag)
        {//손전등을 들고있으면서 클릭했을때
            hiddenPictureFlag = false;
            flashLight.SetActive(true);
            flashLight.transform.SetParent(hiddenPicture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
        }
        else if(inventoryManager.GetSelectedItemName() == "RustedSword" && !hiddenPictureFlag && !isGetPicture)
        {//이미 손전등으로 밝힌 상태에서 선택된아이템이 rustedSword일때
            isGetPicture = true;
            hiddenPicture.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/temp/tempPicture"); //이미지 바꾸기
            nail.SetActive(true);
            piece4.SetActive(true);
            OnQuestCompleted();
        }
    }
    void TurnOffLight()
    {//광원 deactive
        flashLight.SetActive(false);
    }

    public void GetPiece4()
    {
        PlayerPrefs.SetInt(this.name, 4);
        PlayerPrefs.Save();
        piece4.SetActive(false);
    }
}
