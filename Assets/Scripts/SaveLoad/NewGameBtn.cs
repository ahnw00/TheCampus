using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameBtn : MonoBehaviour
{
    [SerializeField] DataManager dataManager;
    [SerializeField] SaveDataClass saveData;
    [SerializeField] GameObject loadingImage;
    [SerializeField] Button continuebtn;
    [SerializeField] GameObject doublecheck;

    [SerializeField] GameObject mainScene;
    [SerializeField] Animator mainStory;
    //[SerializeField] AudioSource lobbyBGM;

    private void Start()
    {
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
    }

    public void OnNewGameClicked()
    {
        if (continuebtn.interactable)
        {
            FindAnyObjectByType<SoundManager>().ChangeBgmClip(null);
            dataManager.DataInitialize();
            Debug.Log(saveData.isNew);
            mainScene.SetActive(true);
            mainStory.Play("MainScene");
        }
        else
        {
            doublecheck.SetActive(true);
        }
    }

    public void OnDoublecheckClicked()
    {
        FindAnyObjectByType<SoundManager>().ChangeBgmClip(null);
        dataManager.DataInitialize();
        Debug.Log(saveData.isNew);
        mainScene.SetActive(true);
        mainStory.Play("MainScene");
    }
}
