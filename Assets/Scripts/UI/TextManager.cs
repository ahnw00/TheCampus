using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private static TextManager instance = null;
    [SerializeField] private TextMeshProUGUI inputField;
    [SerializeField] private GameObject textObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }
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
        while (t < 1f)
        {
            t += Time.deltaTime;
            alpha.a = Mathf.Lerp(1, 0, t);
            backgroundAlpha.a = Mathf.Lerp(1, 0.1f, t);
            inputField.color = alpha;
            textObject.GetComponent<Image>().color = backgroundAlpha;
            yield return null;
        }

        textObject.SetActive(false);
        EnterTexts("");
    }

    public void PopUpText(string text)
    {
        EnterTexts(text);
        StartCoroutine(PopUpCoroutine());
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
