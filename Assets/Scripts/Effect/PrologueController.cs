using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueController : MonoBehaviour
{
    [SerializeField] GameObject eyeImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Expansion()
    {

    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
