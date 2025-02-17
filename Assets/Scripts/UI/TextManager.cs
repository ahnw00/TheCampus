using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private static TextManager instance = null;
    [SerializeField] private TextMeshProUGUI inputField;
    [SerializeField] private GameObject textObject;
    private AudioSource typingSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        typingSFX = textObject.GetComponent<AudioSource>();
    }

    public void EnterTexts(string text)
    {
        inputField.SetText(text);
    }

    IEnumerator PopUpCoroutine()
    {
        textObject.SetActive(true);

        var alpha = inputField.color;
        var backgroundAlpha = textObject.GetComponent<Image>().color;
        var t = 0f;
        while (t < 3.5f)
        {
            t += Time.deltaTime;
            //alpha.a = Mathf.Lerp(1, 0, t);
            ////backgroundAlpha.a = Mathf.Lerp(1, 0.1f, t);
            //inputField.color = alpha;
            //textObject.GetComponent<Image>().color = backgroundAlpha;
            yield return null;
        }

        textObject.SetActive(false);
        EnterTexts("");
    }

    public void PopUpText(string text)
    {
        textObject.SetActive(false);
        EnterTexts(text);
        StopAllCoroutines();
        StartCoroutine(PopUpCoroutine());
        typingSFX.Play();
    }

    public static TextManager TextManager_Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }
}
