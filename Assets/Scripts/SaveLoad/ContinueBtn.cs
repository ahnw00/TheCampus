using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class ContinueBtn : MonoBehaviour
{
    [SerializeField] AudioClip ingameBGM;
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
        SoundManager.Instance.ChangeBgmClip(ingameBGM);
        SceneManager.LoadScene("GameScene");
    }
}
