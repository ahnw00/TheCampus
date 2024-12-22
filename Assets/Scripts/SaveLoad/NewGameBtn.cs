using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtn : MonoBehaviour
{
    public void OnNewGameClicked()
    {
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

        SceneManager.LoadScene("GameScene");
    }
}
