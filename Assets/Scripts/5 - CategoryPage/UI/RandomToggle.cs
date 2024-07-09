using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomToggle : MonoBehaviour
{

    [SerializeField] private Transform On;
    [SerializeField] private Transform Off;
    [SerializeField] private Transform MenuOn;
    [SerializeField] private Transform MenuOff;
    [SerializeField] private int index;
    public StoredData data;
    
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        if (data.ExploreMode == true)
        {
            ON();
        }
        else
        {
            OFF();
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void ON()
    {
        data.ExploreMode = true;
        index = 0;
        Off.gameObject.SetActive(false);
        On.gameObject.SetActive(true);
        MenuOn.gameObject.SetActive(true);
        MenuOff.gameObject.SetActive(false);
        Debug.Log("random mode on");
    }

    public void OFF()
    {
        data.ExploreMode = false;
        index = 1;
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
        MenuOn.gameObject.SetActive(false);
        MenuOff.gameObject.SetActive(true);
        Debug.Log("random mode off");
    }
}
