using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopToggle : MonoBehaviour
{
    [SerializeField]
    GameObject start;
    [SerializeField]
    GameObject pause;
    [SerializeField]
    public bool IsRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        IsRunning = true;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button()
    {
        IsRunning = !IsRunning;
        if (IsRunning)
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }
    
    
    void StartTimer()
    {
        Debug.Log("Started timer");
        pause.SetActive(true);
        start.SetActive(false);
    }
    
    void StopTimer()
    {
        Debug.Log("Stopped timer");
        pause.SetActive(false);
        start.SetActive(true);
    }
}
