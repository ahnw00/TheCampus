using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueController : MonoBehaviour
{
    [SerializeField] GameObject eyeImage;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] List<string> newsText = new List<string>();
    [SerializeField] List<string> speechText = new List<string>();
    private float typingSpeed = 0.05f;
    private int index = 0;

    void Start()
    {
        index = 0;
    }
    void Expansion()
    {

    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }



    void SetText()
    {
        StartCoroutine(TextTyping());
    }

    IEnumerator TextTyping()
    {
        textMesh.text = "";
        for (int i = 0; i < speechText[index].Length; i++)
        {
            textMesh.text += speechText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        index++;
    }
}
