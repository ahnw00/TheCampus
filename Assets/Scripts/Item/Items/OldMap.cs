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
        base.Clicked();
        if (flag == 1)
        {
            MapClicked();
        }
        else Invoke("Delayed", searchingTime + 0.05f);
    }

    void Delayed()
    {
        if(flag == 1)
        {
            MapClicked();
        }
    }

    void MapClicked()
    {
        MiniMap.SetActive(true);
        data.isMapObtained = true;
        dataManager.Save();
        this.gameObject.SetActive(false);
    }
}
