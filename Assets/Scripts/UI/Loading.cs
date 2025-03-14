using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] AudioClip ingameBGM;

    public void LoadScene()
    {
        SoundManager.Instance.ChangeBgmClip(ingameBGM);
        SceneManager.LoadScene("GameScene");
    }
}
