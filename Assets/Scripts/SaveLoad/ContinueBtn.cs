using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class ContinueBtn : MonoBehaviour
{
    private Button continueBtn;

    void Awake()
    {
        continueBtn = GetComponent<Button>();
    }

    void OnEnable()
    {
        if (SaveFileExists())
        {
            continueBtn.interactable = true;
        }
        else
        {
            continueBtn.interactable = false;
        }
    }

    private bool SaveFileExists()
    {
        string savePath = Application.dataPath + "/userData/SaveData.json";
        return File.Exists(savePath);
    }

    public void OnContinueClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
