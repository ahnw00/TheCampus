using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueBtn : MonoBehaviour
{
    public void OnContinueClicked()
    {
        // 만약 StartScene에서 DataManager가 없을 경우를 대비
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
