using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class PrologueController : MonoBehaviour
{
    [SerializeField] GameObject eyeImage;
    [SerializeField] TextMeshProUGUI underText;
    [SerializeField] TextMeshProUGUI centerText;
    [SerializeField] List<string> newsText = new List<string>();
    [SerializeField] List<string> speechText = new List<string>();
    [SerializeField] AudioSource prologueBGM;
    [SerializeField] AudioSource prologueSFX;
    [SerializeField] AudioClip ingameBGM;
    private float typingSpeed = 0.05f;
    private int index = 0;
    private int newsIndex = 0;
    private bool isTyping = false; // 타이핑 중인지 체크

    [SerializeField] GameObject loadingImage;

    void Start()
    {
        index = 0;
        newsIndex = 0;
        centerText.text = "";
        underText.text = "";
    }

    void LoadGameScene()
    {
        FindAnyObjectByType<SoundManager>().ChangeBgmClip(ingameBGM);
        loadingImage.SetActive(true);
    }



    void SetTypingText()
    {
        if (!isTyping && index < speechText.Count)
        {
            StartCoroutine(TextTyping());
        }
    }

    IEnumerator TextTyping()
    {
        isTyping = true;
        centerText.text = "";

        foreach (char letter in speechText[index]) // 한 글자씩 출력
        {
            centerText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        centerText.text += "\n";
        index++;
        foreach (char letter in speechText[index]) // 한 글자씩 출력
        {
            centerText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        index++; // 타이핑 완료 후 다음 문장으로 이동
        isTyping = false;
    }

    void SetNewsText()
    {
        underText.text = newsText[newsIndex++];
    }

    void SetSpeechText()
    {
        centerText.text = speechText[index++];
    }

    void SetSFX(AudioResource sfx)
    {
        prologueSFX.resource = sfx;
        prologueSFX.Play();
    }

    void SetBGM(AudioResource sfx)
    {
        prologueBGM.resource = sfx;
        prologueBGM.Play();
    }
}
