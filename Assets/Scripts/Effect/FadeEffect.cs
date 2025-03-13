using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // ���̵� ȿ���� ����� �̹���

    public void FadeOutIn(float d1, float d2)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(FadeOut(d1));
        StartCoroutine(DelayedFadeIn(d1 + 0.2f, d2));
        Invoke("SetFalse", d1 + d2 + 0.2f);
    }
    private IEnumerator DelayedFadeIn(float delay, float d2)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeIn(d2));
    }

    void SetFalse()
    {
        gameObject.SetActive(false);
    }

    // ���̵���, duration�� ���� �۵�
    public IEnumerator FadeIn(float duration)
    {
        Color color = fadeImage.color;
        float startAlpha = 1; // �ʱ� ���İ�
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0, time / duration); // ���İ��� 0���� ����
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0; // ���� ���İ� ����
        fadeImage.color = color;
    }

    // ���̵�ƿ�
    public IEnumerator FadeOut(float duration)
    {
        Color color = fadeImage.color;
        float startAlpha = 0; // �ʱ� ���İ�
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 1, time / duration); // ���İ��� 1�� �ø�
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1; // ���� ���İ� ����
        fadeImage.color = color;
    }
}

