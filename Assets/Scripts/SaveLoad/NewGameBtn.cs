using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtn : MonoBehaviour
{
    DataManager dataManager;
    [SerializeField] GameObject loadingImage;

    [SerializeField] GameObject mainScene;
    [SerializeField] Animator mainStory;
    //[SerializeField] AudioSource lobbyBGM;

    private void Start()
    {
        dataManager = DataManager.Instance;
    }

    public void OnNewGameClicked()
    {
        SoundManager.Instance.ChangeBgmClip(null);
        dataManager.DataInitialize();
        mainScene.SetActive(true);
        mainStory.Play("MainScene");
    }
}
