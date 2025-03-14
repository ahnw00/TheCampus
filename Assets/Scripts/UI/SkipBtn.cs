using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipBtn : MonoBehaviour
{
    public void Skip()
    {
        SceneManager.LoadScene("GameScene");
    }
}
