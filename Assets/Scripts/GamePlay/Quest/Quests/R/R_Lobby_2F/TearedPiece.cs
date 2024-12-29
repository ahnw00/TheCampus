using UnityEngine;

public class TearedPiece : MonoBehaviour
{
    [SerializeField] private GameObject targetPos;
    private GameObject detectedPos;
    [HideInInspector] public bool foundCorrectPos = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!PlayerPrefs.HasKey(this.name))
        {
            this.gameObject.SetActive(false);
        }
    }
    public void OnDrag()
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnPointerUp()
    {
        if(targetPos == detectedPos)
        {
            this.gameObject.transform.position = targetPos.transform.position;
            foundCorrectPos = true;
        }
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
