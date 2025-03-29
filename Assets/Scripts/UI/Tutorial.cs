using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    DataManager dataManager;
    SaveDataClass saveData;
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;
        gameManager = GameManager.GameManager_Instance;

        if(!saveData.isNew)
            this.gameObject.SetActive(false);
        else
        {
            saveData.isNew = false;
            dataManager.Save();
            gameManager.TurnOnUI();
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
        gameManager.TurnOffUI();
    }
}
