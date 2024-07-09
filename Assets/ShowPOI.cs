using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPOI : MonoBehaviour
{
    private StoredData datas;
    
    
    [SerializeField]private Image POIPic;

    [SerializeField] private int Index;
    // Start is called before the first frame update
    void Start()
    {
        datas = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            POIPic.sprite = datas.Images[Index];
        }
        catch (Exception)
        {
           Destroy(this.gameObject);
        }
        
    }
}
