using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class ContinueBtn : MonoBehaviour
{
    [SerializeField] DataManager dataManager;
    SaveDataClass saveData;
    [SerializeField] AudioClip ingameBGM;
    [SerializeField] GameObject loadingImage;
    private Button continueBtn;

    void Awake()
    {
        continueBtn = GetComponent<Button>();
    }

    private void Start()
    {
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;

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
        Debug.Log(saveData.isNew);
        return !saveData.isNew;
    }

    public void OnContinueClicked()
    {
        loadingImage.SetActive(true);
        Debug.Log("asdf");
        FindAnyObjectByType<SoundManager>().ChangeBgmClip(null);
    }
}
