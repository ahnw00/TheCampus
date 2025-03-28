using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public Image noteImage;

    void Update()
    {
        noteImage = GetComponent<Image>();
        noteImage.SetNativeSize();
    }
}
