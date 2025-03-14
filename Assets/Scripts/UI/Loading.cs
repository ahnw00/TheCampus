using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] AudioClip ingameBGM;
    float time = 3f;

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene()
    {
        FindAnyObjectByType<SoundManager>().ChangeBgmClip(ingameBGM);
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator FadeIn()
    {
        Color color = this.GetComponent<Image>().color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            this.GetComponent<Image>().color = color;
            yield return null;
        }
        LoadScene();
    }
}
