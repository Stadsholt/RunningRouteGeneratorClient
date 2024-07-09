using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGPSData : MonoBehaviour
{
    [SerializeField] public float userLat;
    [SerializeField] public float userLon;
    [SerializeField] public float userBearing;

    [SerializeField]
    bool GPSOn;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        GPSOn = false;
        yield return new WaitForSeconds(1);

        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location not enabled");
            yield break;
        }

        // Starts the location service.
        Input.location.Start(0.5f,0.5f);
        yield return new WaitForSeconds(1);

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            GPSOn = true;
            Input.compass.enabled = true;
            //InvokeRepeating("GetLatLon", 1f, 1f);
        }
        
        

        // Stops the location service if there is no need to query location updates continuously.
        //Input.location.Stop();
    }

    void Update()
    {
        GetLatLon();
    }

    public void GetLatLon()
    {
        if (!GPSOn) return;
        
        userLat = Input.location.lastData.latitude;
        userLon = Input.location.lastData.longitude;
        userBearing = Input.compass.trueHeading; 
        Debug.Log($"{userLat}:{userLon}");
        Debug.Log(Input.location.status);
        
    }
}
