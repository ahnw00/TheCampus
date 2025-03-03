using UnityEngine;

public class EndingBtn : MonoBehaviour
{
    private DataManager dataManager;
    private SaveDataClass saveData;

    [SerializeField] GameObject EndingAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataManager = DataManager.Instance;
        saveData = dataManager.saveData;

        if (!saveData.gameCompleted)
            this.gameObject.SetActive(false);
    }

    public void TurnOnEndingAnim()
    {
        EndingAnim.SetActive(true);
    }
}
