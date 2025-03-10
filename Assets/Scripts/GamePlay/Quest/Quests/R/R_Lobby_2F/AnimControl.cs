using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimControl : MonoBehaviour
{
    [SerializeField] private GameObject btn;
    [SerializeField] private TextMeshProUGUI animText;

    public void TurnOnBtn()
    {
        btn.GetComponent<Button>().enabled = true;
    }

    public void SetText(string str)
    {
        animText.text = str;
    }
}
