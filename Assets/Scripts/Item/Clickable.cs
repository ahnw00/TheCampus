using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Clickable : MonoBehaviour
{
    protected int flag = 0;
    protected float searchingTime = 1f; //게이지 차는데 걸리는 시간
    private GameObject gaugeObj;
    private Image gauge;
    string ObjectID;

    private void Start()
    {
        gaugeObj = GameManager.GameManager_Instance.gaugeObject;
        gauge = GameManager.GameManager_Instance.gaugeImage;
    }

    public virtual void Clicked()
    {
        ObjectID = this.gameObject.name;
        if (this.gameObject.GetComponent<ObtainableItem>())
        {//아이템인경우
            ObjectID = this.gameObject.GetComponent<ObtainableItem>().GetItemKey();
        }

        if(gauge == null)
        {
            gaugeObj = GameManager.GameManager_Instance.gaugeObject;
            gauge = GameManager.GameManager_Instance.gaugeImage;
        }
        //변수가 저장되어있지 않으면 저장해줘
        if(!PlayerPrefs.HasKey(ObjectID))
        {
            PlayerPrefs.SetInt(ObjectID, 0);
            PlayerPrefs.Save();
        }
        //0이면 처음 클릭했을때, 1이면 클릭후 탐색 완료했을때, 2면 탐색완료 후 주웠을때
        flag = PlayerPrefs.GetInt(ObjectID);
        
        //한번도 클릭되지 않았을 때
        if(flag == 0)
        {
            Vector3 inputPos = GameManager.GameManager_Instance.cam.ScreenToWorldPoint(Input.mousePosition);
            float targetX = inputPos.x;
            float targetY = inputPos.y;
            Vector3 targetPos = new Vector3(targetX, targetY, 0);
            gaugeObj.GetComponent<RectTransform>().position = targetPos;
            if (targetX < 0)
                gaugeObj.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            else
                gaugeObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            gaugeObj.gameObject.SetActive(true);
            GameManager.GameManager_Instance.GetClickSFX(0).Play();
            StartCoroutine(SearchingCoroutine());
        }
        else
        {
            GameManager.GameManager_Instance.GetClickSFX(1).Play();
        }
    }

    IEnumerator SearchingCoroutine()
    {
        float time = 0;
        while(time < searchingTime && Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            gauge.fillAmount = time / searchingTime;
            yield return null;
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            GameManager.GameManager_Instance.GetClickSFX(0).Stop();
        }

        if(time > searchingTime - 0.05f)
        {
            PlayerPrefs.SetInt(ObjectID, 1);
            flag = 1;
            PlayerPrefs.Save();
        }

        gaugeObj.gameObject.SetActive(false);
        gauge.fillAmount = 0f;
    }
}
