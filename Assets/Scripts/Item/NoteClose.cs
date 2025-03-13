using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoteClose : MonoBehaviour
{
    [SerializeField] GameObject noteImageObj;
    [SerializeField] Image closeBtn;
    [SerializeField] Image backgroundObj;
    [SerializeField] Image noteObj;

    [SerializeField] private float fadeOutDuration = 1f;
    public void Close()
    {
        StartCoroutine(FadeOutSequence());
    }

    IEnumerator FadeOutSequence()
    {
        StartCoroutine(FadeOut(backgroundObj, fadeOutDuration));
        StartCoroutine(FadeOut(noteObj, fadeOutDuration));
        StartCoroutine(FadeOut(closeBtn, fadeOutDuration));
        yield return new WaitForSeconds(fadeOutDuration);
        noteImageObj.SetActive(false);
    }

    IEnumerator FadeOut(Image obj, float duration)
    {
        Color color = obj.color;
        float startAlpha = 1;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0, time / duration);
            obj.color = color;
            yield return null;
        }

        color.a = 0;
        obj.color = color;
    }
}
