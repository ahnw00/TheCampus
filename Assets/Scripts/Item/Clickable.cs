﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Clickable : MonoBehaviour
{
    protected int flag = 0;
    private Image gauge;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        gauge = GameManager.GameManager_Instance.gauge;
    }

    public virtual void Clicked()
    {
        if(gauge == null)
        {
            gauge = GameManager.GameManager_Instance.gauge;
        }
        //변수가 저장되어있지 않으면 저장해줘
        if(!PlayerPrefs.HasKey(this.gameObject.name))
        {
            PlayerPrefs.SetInt(this.gameObject.name, 0);
            PlayerPrefs.Save();
        }
        flag = PlayerPrefs.GetInt(this.gameObject.name);
        
        //한번도 클릭되지 않았을 때
        if(PlayerPrefs.GetInt(this.gameObject.name) == 0)
        {
            gauge.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
            gauge.gameObject.SetActive(true);
            StartCoroutine(SearchingCoroutine());
        }
    }

    IEnumerator SearchingCoroutine()
    {
        float time = 0;
        while(time < 3f && Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            gauge.fillAmount = time / 3f;
            yield return null;
        }

        if(time > 2.95f)
        {
            PlayerPrefs.SetInt(this.gameObject.name, 1);
            flag = 1;
            PlayerPrefs.Save();
        }

        gauge.gameObject.SetActive(false);
        gauge.fillAmount = 0f;
    }
}
