using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BikeToggle : MonoBehaviour
{
    public StoredData data;
    [SerializeField] private Transform Foot;
    [SerializeField] private Transform Bike;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        if (data.BikeMode == true)
        {
            BikeSelected();
        }
        else
        {
            WalkSelected();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }   
     
    public void WalkSelected()
    {
        data.BikeMode = false;
        Foot.gameObject.SetActive(true);
        Bike.gameObject.SetActive(false);
        data.DrivingProfile = "foot-walking";
        Debug.Log("Changed mode to: " + data.DrivingProfile);

    }

    public void BikeSelected()
    {
        data.BikeMode = true;
        Foot.gameObject.SetActive(false);
        Bike.gameObject.SetActive(true);
        data.DrivingProfile = "cycling-regular";
        Debug.Log("Changed mode to: " + data.DrivingProfile);

    }
}
