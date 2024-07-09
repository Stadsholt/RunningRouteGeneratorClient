using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSLocation : MonoBehaviour
{
    [SerializeField] private string gpsStatus;
    public string GPSStatus { get { return gpsStatus; } }
    [Tooltip("Same as X coordinates")]
    [SerializeField] private float latitudeValue;
    [Tooltip("Same as Y coordinates")]
    [SerializeField] private float longitudeValue;
    [SerializeField] private float altitudeValue;
    [SerializeField] private float horizontalAccuracyValue;
    [SerializeField] private double timeStampValue;

    [SerializeField] private float heading;
    [SerializeField] private Vector2 location;
    public float Heading { get { return heading; } }
    public Vector2 Location { get { return location;} }

    private void Start()
    {
        Input.compass.enabled = true;
        StartCoroutine(GPSLoc());
    }

    IEnumerator GPSLoc()
    {
        //Checking if user has location enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        //Starts location
        Input.location.Start();

        //Location initailize
        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //Location not init
        if(maxWait < 1)
        {
            gpsStatus = "Time out";
            yield break;
        }

        //Connection failed
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            gpsStatus = "Unable to determine device location";
            yield break;
        }
        else
        {
            gpsStatus = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if(Input.location.status == LocationServiceStatus.Running)
        {
            gpsStatus = "Running";
            location = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            heading = Input.compass.trueHeading;
            altitudeValue = Input.location.lastData.altitude;
            horizontalAccuracyValue = Input.location.lastData.horizontalAccuracy;
            timeStampValue = Input.location.lastData.timestamp;
        }
        else
        {
            gpsStatus = "Stop";
        }
    }
}
