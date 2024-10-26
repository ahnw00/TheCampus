using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreManager SM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = FindAnyObjectByType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
