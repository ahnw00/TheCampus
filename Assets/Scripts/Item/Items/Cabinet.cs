using UnityEngine;

public class Cabinet : Clickable
{
    private GameManager gameManager;

    [SerializeField] private GameObject invenBackground;
    [SerializeField] private GameObject invenSlots;
    [SerializeField] private GameObject cabinet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.GameManager_Instance;
    }

    public override void Clicked()
    {
        base.Clicked();
        if (flag == 1)
        {
            OpenCabinet();
        }
        else Invoke("Delayed", searchingTime + 0.05f);
    }

    void Delayed()
    {
        if (flag == 1)
        {
            OpenCabinet();
        }
    }

    void OpenCabinet()
    {
        gameManager.TurnOnUI();
        invenBackground.SetActive(true);
        invenSlots.SetActive(true);
        cabinet.SetActive(true);
        invenSlots.GetComponent<RectTransform>().anchoredPosition = new Vector3(200f, 120f, 0f);
    }
}
