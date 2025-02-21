using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenObtainableItem : ObtainableItem
{
    [SerializeField] string requiredItem;
    public override void PopUpObtainPanel()
    {// ���õ� �������� �ʿ��� �������� ���� ���� �� �ִ�.
        if (inventoryManager.GetSelectedItemName() == requiredItem)
        {
            base.PopUpObtainPanel();
            
            string flashlightDialogue = DialogueManager.DialoguManager_Instance.GetSystemDialogue("인게임 대사", 2);
            TextManager.TextManager_Instance.PopUpText(flashlightDialogue);
            Record.Record_Instance.AddText(TextType.Speaker, flashlightDialogue, "나");
        }
        else
        {
            TextManager.TextManager_Instance.PopUpText(DialogueManager.DialoguManager_Instance.GetSystemDialogue("시스템 대사", 3));
        }
    }

    protected override void ObtainItem()
    {
        base.ObtainItem();
        Invoke("GetPiece", 2f);
    }
    protected void GetPiece()
    {
        PlayerPrefs.SetInt("Piece2", 1);
        PlayerPrefs.Save();
        PrintGetPieceDialogue();
    }
}
