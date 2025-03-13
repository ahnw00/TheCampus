using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimControl : MonoBehaviour
{
    [SerializeField] private GameObject btn;
    [SerializeField] private TextMeshProUGUI animText;
    [SerializeField] private GameObject endingCredit;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.Instance;
    }

    public void TurnOnBtn()
    {
        btn.GetComponent<Button>().enabled = true;
    }

    public void SetText(string str)
    {
        animText.text = str;
    }

    public void ChangeBGMClip(AudioClip clip)
    {
        soundManager.ChangeBgmClip(clip);
    }

    public void ChangeSFXClip(AudioClip clip)
    {
        soundManager.ChangeSfxClip(clip);
    }

    public void StopBGM()
    {
        soundManager.StopBGM();
    }

    public void PopUpCredit()
    {
        endingCredit.SetActive(true);
    }
}
