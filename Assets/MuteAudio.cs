using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    
    [SerializeField] private AudioListener Listener;
    // Start is called before the first frame update
    void Start()
    {
        Listener.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteToggle()
    {
        if (Listener.enabled == true)
        {
            Listener.enabled = false;
        }
        else
        {
            Listener.enabled = true;
        }
    }
    


}
