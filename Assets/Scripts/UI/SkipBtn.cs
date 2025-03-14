using UnityEngine;

public class SkipBtn : MonoBehaviour
{
    [SerializeField] GameObject LoadingImage;

    public void Skip()
    {
        LoadingImage.SetActive(true);
    }
}
