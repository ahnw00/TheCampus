using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance = null;
    [SerializeField] private GameObject inventory;
    public bool isInvenOpened;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("MapManager duplicate error, destroying duplicate instance.");
            //Destroy(this.gameObject);
        }
        isInvenOpened = inventory.activeSelf;
    }

    public void CheckInvenOpened()
    {
        if(!isInvenOpened) isInvenOpened = true;
        else isInvenOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static InventoryManager InvenManager_Instance
    {
        get
        {
            if (null == instance)
            {
                Debug.Log("MapManager is null");
                return null;
            }
            return instance;
        }
    }
}
