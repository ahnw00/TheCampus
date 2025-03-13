using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainBtn : MonoBehaviour
{
    [SerializeField] AudioClip lobbyBGM;
    public void ToStartScene()
    {
        SoundManager.Instance.ChangeBgmClip(lobbyBGM);
        SoundManager.Instance.SaveVolume();
        SceneManager.LoadScene("StartScene");
    }
}