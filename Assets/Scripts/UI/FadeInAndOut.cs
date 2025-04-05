using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAndOut : MonoBehaviour
{
    float time = 1f;
    [SerializeField] List<Image> imageList;

    private void OnEnable()
    {
        ResetImageList();
        SetAlpha();
        StartCoroutine(FadeIn());
    }

    void ResetImageList()
    {
        imageList.Clear();
        AddList();
    }

    void SetAlpha()
    {
        foreach(var img in imageList)
        {
            Color c = img.color;
            c.a = 0f;
            img.color = c;
        }
    }

    void AddList()
    {
        var allChildren = GetComponentsInChildren<Transform>();
        foreach (var child in allChildren)
        {
            if (child.GetComponent<Image>())
            {
                Image temp = child.GetComponent<Image>();
                imageList.Add(temp);
            }
        }
    }

    IEnumerator FadeIn()
    {
        float gauge = 0f;

        while (gauge < time)
        {
            foreach(var image in imageList)
            {
                if(image)
                {
                    Color color = image.color;
                    color.a += Time.deltaTime / time;
                    image.color = color;
                }
            }
            gauge += Time.deltaTime / time;
            yield return null;
        }
    }

    public void SetOffObj()
    {
        ResetImageList();
        GameManager.GameManager_Instance.TurnOffUI();
        if(this.enabled)
            StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float gauge = 0f;

        while (gauge < time)
        {
            foreach (var image in imageList)
            {
                if (image)
                {
                    Color color = image.color;
                    color.a -= Time.deltaTime / time;
                    image.color = color;
                }
            }
            gauge += Time.deltaTime / time;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
