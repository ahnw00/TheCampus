using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private static TextManager instance = null;
    [SerializeField] private TextMeshProUGUI inputField;

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
        inputField.gameObject.SetActive(true);

        var alpha = inputField.color;
        var t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            alpha.a = Mathf.Lerp(0, 1, t);
            inputField.color = alpha;
            yield return null;
        }

        inputField.gameObject.SetActive(false);
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
