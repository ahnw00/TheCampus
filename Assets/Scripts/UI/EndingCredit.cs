using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public void TheEnd()
    {
        SceneManager.LoadScene("StartScene");
    }
}
