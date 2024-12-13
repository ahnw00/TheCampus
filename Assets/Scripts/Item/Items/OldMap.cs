using UnityEngine;

public class OldMap : Clickable
{
    DataManager dataManager;
    SaveDataClass data;
    [SerializeField] GameObject MiniMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataManager = DataManager.Instance;
        data = dataManager.saveData;
        
        if(data.isMapObtained)
        {
            MiniMap.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public override void Clicked()
    {
        Debug.Log("Clicked");

        MiniMap.SetActive(true);
        data.isMapObtained = true;
        dataManager.Save();
        this.gameObject.SetActive(false);
    }
}
