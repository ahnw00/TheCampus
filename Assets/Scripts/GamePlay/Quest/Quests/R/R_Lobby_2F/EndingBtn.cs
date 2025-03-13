using UnityEngine;

public class EndingBtn : Clickable
{
    private DataManager dataManager;
    private SaveDataClass saveData;

    [SerializeField] GameObject EndingAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flag = 1;
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;

        if (!saveData.gameCompleted)
            this.gameObject.SetActive(false);
    }

    public override void Clicked()
    {
        base.Clicked();
        EndingAnim.SetActive(true);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
