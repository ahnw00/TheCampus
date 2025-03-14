using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtn : MonoBehaviour
{
    DataManager dataManager;
    SaveDataClass saveData;

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
        SoundManager.Instance.ChangeBgmClip(null);
        dataManager.DataInitialize();
        saveData.isNew = false;
        dataManager.Save();
        mainScene.SetActive(true);
        mainStory.Play("MainScene");
    }
}
