using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtn : MonoBehaviour
{
    [SerializeField] GameObject mainScene;
    [SerializeField] Animator mainStory;
    //[SerializeField] AudioSource lobbyBGM;
    public void OnNewGameClicked()
    {
        SoundManager.Instance.ChangeBgmClip(null);
        // ���� StartScene���� DataManager�� ���� ��츦 ���
        if (DataManager.Instance == null)
        {
            Debug.LogWarning("DataManager.Instance is null. Initializing new DataManager.");
            GameObject dataManagerObject = new GameObject("DataManager");
            DataManager dataManager = dataManagerObject.AddComponent<DataManager>();
        }

        else
        {
            DataManager.Instance.DataInitialize();
            Debug.Log("Data successfully initialized");
        }
        //lobbyBGM.enabled = false;
        mainScene.SetActive(true);
        mainStory.Play("MainScene");
    }
}
