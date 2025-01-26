using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainBtn : MonoBehaviour
{
    public void ToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}