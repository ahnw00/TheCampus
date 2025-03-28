using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public Camera cam;
    private RaycastHit2D hit;
    private Vector3 rayDir = Vector3.forward;
    private Vector3 mousePos;
    public int isUiOpened = 0; //***** ui껐다 킬때 바꿔주어야함
    public GameObject gaugeObject;
    [SerializeField] GameObject gaugeSound;
    public Image gaugeImage;

    [SerializeField] private GameObject map;
    [SerializeField] private GameObject miniMapMask;
    [SerializeField] private GameObject miniMapOutline;
    [SerializeField] private GameObject radio;

    public bool Reset;
    SoundManager soundManager;
    [SerializeField] private AudioClip mapClip;
    [SerializeField] private AudioClip btnClip;
    private AudioSource[] ClickSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }
        //cam = FindAnyObjectByType<Camera>();
    }

    private void Start()
    {
        ClickSFX = gaugeSound.GetComponents<AudioSource>();
        soundManager = SoundManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            //Debug.DrawRay(mousePos, rayDir * 10, Color.red, 0.5f);
            hit = Physics2D.Raycast(mousePos, rayDir, 10f);
            //Debug.Log(hit.collider.name);
            DetectedFunction();
        }
    }

    public void TurnOnUI()
    {
        isUiOpened++;
        soundManager.ChangeSfxClip(btnClip);
    }

    public void TurnOffUI()
    {
        soundManager.ChangeSfxClip(btnClip);
        if (isUiOpened > 0)
            isUiOpened--;
    }

    void DetectedFunction()
    {
        if(hit && hit.collider.GetComponent<Clickable>() && isUiOpened == 0)
        {
            Clickable obj = hit.collider.GetComponent<Clickable>();
            obj.Clicked();
        }
    }

    public void PopUpMiniMap()
    {
        soundManager.ChangeSfxClip(mapClip);
        miniMapMask.SetActive(false);
        miniMapOutline.SetActive(false);
        Vector3 temp = map.transform.position;
        cam.transform.position = new Vector3(temp.x, temp.y + 3f, -10f);
        cam.orthographicSize = 42f;
    }
    public void PopOutMiniMap()
    {
        soundManager.ChangeSfxClip(mapClip);
        miniMapOutline.SetActive(true);
        miniMapMask.SetActive(true);
        cam.transform.position = new Vector3(0, 0, -10f);
        cam.orthographicSize = 5.4f;
    }

    public void TurnOnRadio()
    {
        radio.SetActive(true);
    }

    public static GameManager GameManager_Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }

    public AudioSource GetClickSFX(int index)
    {
        return ClickSFX[index];
    }
}
