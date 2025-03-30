using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.GameManager_Instance;

        if(PlayerPrefs.HasKey(this.gameObject.name))
            this.gameObject.SetActive(false);
        else
        {
            PlayerPrefs.SetInt(this.gameObject.name, 1);
            PlayerPrefs.Save();
            gameManager.isUiOpened++;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        Image imageComponent = this.GetComponent<Image>();
        var alpha = imageComponent.color;
        var t = 0f;
        float length = 2f;
        while (t < length)
        {
            t += Time.deltaTime;
            alpha.a = Mathf.Lerp(1, 0, t);
            imageComponent.color = alpha;
            yield return null;
        }

        this.gameObject.SetActive(false);
        gameManager.isUiOpened--;
    }
}
