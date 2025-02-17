using UnityEngine;

public class LawImage : MonoBehaviour
{
    [SerializeField] private Sprite completedImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerPrefs.GetInt(this.name) == 1)
            this.GetComponent<SpriteRenderer>().sprite = completedImage;
    }
}
