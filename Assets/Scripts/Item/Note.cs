using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Note : Clickable
{
    [SerializeField] GameObject noteImageObj;
    [SerializeField] Image closeBtn;
    [SerializeField] Image backgroundObj;
    [SerializeField] Image noteObj;
    [SerializeField] Sprite backgroundImage;
    [SerializeField] Sprite noteImage;

    [SerializeField] private float fadeInDuration = 0.5f;
    public override void Clicked()
    {
        base.Clicked();
        if (flag == 1)
        {
            noteImageObj.SetActive(true);
            backgroundObj.sprite = backgroundImage;
            noteObj.sprite = noteImage;
            GameManager.GameManager_Instance.TurnOnUI();
            StartCoroutine(FadeInSequence());
        }
        else Invoke("Delayed", searchingTime + 0.05f);

    }

    void Delayed()
    {
        if (flag == 1)
        {
            noteImageObj.SetActive(true);
            backgroundObj.sprite = backgroundImage;
            noteObj.sprite = noteImage;
            GameManager.GameManager_Instance.TurnOnUI();
            StartCoroutine(FadeInSequence());
        }
    }

    IEnumerator FadeInSequence()
    {
        noteObj.color = new Color(1, 1, 1, 0);
        // 1️⃣ 배경 페이드인 (0.5s)
        StartCoroutine(FadeIn(backgroundObj, fadeInDuration));
        StartCoroutine(FadeIn(closeBtn, fadeInDuration));
        yield return new WaitForSeconds(0.5f);
        // 2️⃣ 스토리노트 페이드인 (0.5s)
        yield return StartCoroutine(FadeIn(noteObj, fadeInDuration));
    }

    IEnumerator FadeIn(Image obj, float duration)
    {
        Color color = obj.color;
        float startAlpha = 0;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 1, time / duration);
            obj.color = color;
            yield return null;
        }

        color.a = 1;
        obj.color = color;
    }
}
