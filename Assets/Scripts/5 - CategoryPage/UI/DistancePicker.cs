using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistancePicker : MonoBehaviour
{

    //[SerializeField] private GameObject InputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Distance()
    {
        string distancePicked = this.gameObject.GetComponentInChildren<TMP_InputField>().text;
        Debug.Log("Number of POI picked" + distancePicked);
    }
}
