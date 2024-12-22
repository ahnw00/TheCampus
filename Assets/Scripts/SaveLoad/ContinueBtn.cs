using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueBtn : MonoBehaviour
{
    public void OnContinueClicked()
    {
        // ���� StartScene���� DataManager�� ���� ��츦 ���
        if(DataManager.Instance == null)
        {
            Debug.LogWarning("DataManager.Instance is null. Initializing new DataManager.");
            GameObject dataManagerObject = new GameObject("DataManager");
            DataManager dataManager = dataManagerObject.AddComponent<DataManager>();
        }

        else
        {
            Debug.Log("Data successfully loaded.");
        }

        SceneManager.LoadScene("GameScene");
    }
}
