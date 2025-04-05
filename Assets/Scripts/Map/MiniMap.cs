using System.Collections;
using UnityEngine;

public class MiniMap : Clickable
{
    float timeToMove = 0.5f;
    Vector3 smallScale = new Vector3(8.5f, 8.5f, 1f);
    Vector3 largeScale = new Vector3(9f, 9f, 1f);

    public override void Clicked()
    {
        GameManager.GameManager_Instance.PopOutMiniMap();
    }

    public IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.localScale = Vector3.Lerp(smallScale, largeScale, t);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.localScale = Vector3.Lerp(largeScale, smallScale, t);
            yield return null;
        }
    }
}
