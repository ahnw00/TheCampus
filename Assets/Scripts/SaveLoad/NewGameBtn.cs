using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtn : MonoBehaviour
{
    [SerializeField] DataManager dataManager;
    [SerializeField] SaveDataClass saveData;
    [SerializeField] GameObject loadingImage;

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
        FindAnyObjectByType<SoundManager>().ChangeBgmClip(null);
        dataManager.DataInitialize();
        Debug.Log(saveData.isNew);
        mainScene.SetActive(true);
        mainStory.Play("MainScene");
    }
}
