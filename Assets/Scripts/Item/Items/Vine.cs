using UnityEngine;

public class Vine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerPrefs.GetInt(this.name) == 1)
            this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(this.name) == 1)
            this.gameObject.SetActive(false);
    }
}
