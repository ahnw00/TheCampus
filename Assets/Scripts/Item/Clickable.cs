using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class Clickable : MonoBehaviour
{
    protected int flag = 0;
    protected float searchingTime = 1f; //게이지 차는데 걸리는 시간
    private Image gauge;
    string ObjectID;

    private void Start()
    {
        gauge = GameManager.GameManager_Instance.gauge;
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
            gauge = GameManager.GameManager_Instance.gauge;
        }
        //변수가 저장되어있지 않으면 저장해줘
        if(!PlayerPrefs.HasKey(ObjectID))
        {
            PlayerPrefs.SetInt(ObjectID, 0);
            PlayerPrefs.Save();
        }
        flag = PlayerPrefs.GetInt(ObjectID);
        
        //한번도 클릭되지 않았을 때
        if(PlayerPrefs.GetInt(ObjectID) == 0)
        {
            Vector3 inputPos = GameManager.GameManager_Instance.cam.ScreenToWorldPoint(Input.mousePosition);
            float targetX = inputPos.x;
            float targetY = inputPos.y;
            Vector3 targetPos = new Vector3(targetX, targetY, 0);
            gauge.gameObject.GetComponent<RectTransform>().position = targetPos;
            gauge.gameObject.SetActive(true);
            StartCoroutine(SearchingCoroutine());
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

        if(time > searchingTime - 0.05f)
        {
            PlayerPrefs.SetInt(ObjectID, 1);
            flag = 1;
            PlayerPrefs.Save();
        }

        gauge.gameObject.SetActive(false);
        gauge.fillAmount = 0f;
    }
}
