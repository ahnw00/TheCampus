using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitionHall : Quest
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject flashLight; //����
    [SerializeField] GameObject picture; // �Ϲݱ׸�
    [SerializeField] GameObject hiddenPicture; // ������ �׸�
    [SerializeField] GameObject nail;
    [SerializeField] GameObject piece4;

    //Ŭ���� �Ǵ��ϴ� flag
    bool pictureFlag = true;
    bool hiddenPictureFlag = true;
    bool isGetPicture = false;
    public override void Start()
    {
        questName = "ExhibitionHall_SubQuest";
    }

    public override void StartQuest()
    {//����Ʈ�� �����Ҷ� ����
        questInven.SetActive(true);
        if (questStatus == QuestStatus.NotStarted)
        {
            questStatus = QuestStatus.InProgress;
            inventoryManager = InventoryManager.InvenManager_Instance;
        }
        inventoryManager.SetItemsOnInven(slotList);
    }

    public override void ifQuestBtnClicked()
    {//quest��ư�� ������ ������ ����Ǵ� �Լ�. ���⼭�� �κ��� �ҷ��´�.
        base.ifQuestBtnClicked();
    }
    protected override void OnQuestCompleted()
    {
        this.questStatus = QuestStatus.Completed;
        Debug.Log(questName + "clear");
    }

    public void OnPictureClicked()
    {//�Ϲݱ׸� Ŭ���� �Һ� ��񳪿��� ������� �Լ�
        if(inventoryManager.GetSelectedItemName() == "Flashlight" && pictureFlag)
        {
            pictureFlag = false;
            flashLight.SetActive(true);
            flashLight.transform.SetParent(picture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
            Invoke("TurnOffLight", 1f); //����
        }
    }
    public void OnHiddenPictureClicked()
    {//������ �׸� Ŭ���� �����Լ�
        if (inventoryManager.GetSelectedItemName() == "Flashlight" && hiddenPictureFlag)
        {//�������� ��������鼭 Ŭ��������
            hiddenPictureFlag = false;
            flashLight.SetActive(true);
            flashLight.transform.SetParent(hiddenPicture.transform, false);
            //flashLight.transform.localPosition = Vector3.zero;
        }
        else if(inventoryManager.GetSelectedItemName() == "RustedSword" && !hiddenPictureFlag && !isGetPicture)
        {//�̹� ���������� ���� ���¿��� ���õȾ������� rustedSword�϶�
            isGetPicture = true;
            hiddenPicture.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/temp/tempPicture"); //�̹��� �ٲٱ�
            nail.SetActive(true);
            piece4.SetActive(true);
            OnQuestCompleted();
        }
    }
    void TurnOffLight()
    {//���� deactive
        flashLight.SetActive(false);
    }

    public void GetPiece4()
    {
        PlayerPrefs.SetInt(this.name, 4);
        PlayerPrefs.Save();
        piece4.SetActive(false);
    }
}
