using UnityEngine;

public class TearedPiece : MonoBehaviour
{
    [SerializeField] private TearedMap pieceQuest;
    [SerializeField] private GameObject targetPos;
    private GameObject detectedPos;
    [HideInInspector] public bool foundCorrectPos = false;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!PlayerPrefs.HasKey(this.name))
        {
            this.gameObject.SetActive(false);
        }
        cam = GameManager.GameManager_Instance.cam;
    }
    public void OnDrag()
    {
        Vector3 dragPos = cam.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(dragPos.x, dragPos.y, 0f);
    }

    public void OnPointerUp()
    {
        if (targetPos == detectedPos)
        {
            this.gameObject.transform.position = targetPos.transform.position;
            foundCorrectPos = true;
            pieceQuest.Check();
        }
        else foundCorrectPos = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedPos = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedPos = null;
    }
}
