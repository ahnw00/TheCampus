using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationAndTodoList : MonoBehaviour
{
    private static LocationAndTodoList instance = null;
    [SerializeField] private TextMeshProUGUI currentLocation; //현재 위치 텍스트 오브젝트
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject toDoListPrefab; //할일 프리펩
    [SerializeField] private Transform content; //todolist
    private Dictionary<string, string> translate = new Dictionary<string, string>(); //번역 사전
    private Dictionary<string, GameObject> toDoList = new Dictionary<string, GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        currentLocation.SetText("???");

        translate.Add("R_Lobby_1F", "R동 로비 1층");
        translate.Add("R_Lobby_2F", "R동 로비 2층");
        translate.Add("CafeNamu", "카페나무");
        translate.Add("LawClassroom", "법학 강의실");
        translate.Add("ExhibitionHall", "전시관");
        translate.Add("Playground", "운동장");
        translate.Add("H_Lobby", "H동 로비");
    }

    public void SetLocation(string location)
    {//현재 위치를 갱신하는 함수
        if (minimap.activeInHierarchy)
        {
            location = ConvertToKorean(location);
            currentLocation.SetText(location);
        }
        else currentLocation.SetText("???");
    }

    private string ConvertToKorean(string location)
    {//Nodeclass를 한글로 번역
        string result;
        if (translate.ContainsKey(location))
        {
            result = translate[location];
        }
        else
        {//없으면 그냥 그대로 출력
            result = location;
        }
        return result;
    }

    // Update is called once per frame
    public static LocationAndTodoList LocationAndTodoList_Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }

    public void SetTodo(string key, string description)
    {
        GameObject newTodo = Instantiate(toDoListPrefab, content);
        TextMeshProUGUI todoText = newTodo.GetComponentInChildren<TextMeshProUGUI>();
        todoText.SetText(description);
        toDoList.Add(key, newTodo);
    }

    public void DeleteTodo(string key)
    {
        GameObject destoryTodo = toDoList[key];
        toDoList.Remove(key);
        Destroy(destoryTodo);
    }
}
