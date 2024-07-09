using System.Collections;
using System.Globalization;
using UnityEngine;

public class GPSToggle : MonoBehaviour
{
    private bool isLocationReady = false;

    void Start()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

        Input.compass.enabled = true;
        Input.location.Start();
        StartCoroutine(WaitForLocation());
    }

    void OnApplicationQuit()
    {
        StopGPSAndCompass();
    }

    void OnDestroy()
    {
        StopGPSAndCompass();
    }
    
    void StopGPSAndCompass()
    {
        Input.compass.enabled = false;
        Input.location.Stop();
        isLocationReady = false;
    }

    IEnumerator WaitForLocation()
    {
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return null;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("Failed to initialize location service.");
            yield break;
        }

        isLocationReady = true;
    }

    public bool IsLocationReady()
    {
        return isLocationReady;
    }
}
