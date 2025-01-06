using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void OnQuitToMainClicked()
    {
        if (SoundManager.Instance != null)
        {
            //SoundManager.Instance.OnSceneChange();
        }

        SceneManager.LoadScene("StartScene");
    }
}
