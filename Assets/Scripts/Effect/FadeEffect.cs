using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // 페이드 효과에 사용할 이미지

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

    // 페이드인, duration초 동안 작동
    public IEnumerator FadeIn(float duration)
    {
        Color color = fadeImage.color;
        float startAlpha = 1; // 초기 알파값
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0, time / duration); // 알파값을 0으로 줄임
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0; // 최종 알파값 설정
        fadeImage.color = color;
    }

    // 페이드아웃
    public IEnumerator FadeOut(float duration)
    {
        Color color = fadeImage.color;
        float startAlpha = 0; // 초기 알파값
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 1, time / duration); // 알파값을 1로 늘림
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1; // 최종 알파값 설정
        fadeImage.color = color;
    }
}

