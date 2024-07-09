using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSpentcehck : MonoBehaviour
{
    // Start is called before the first frame update
    private StoredData _data;

    [SerializeField]private TMP_Text TimeSpentBox;
    [SerializeField]private TMP_Text DistanceOnRoute;
    void Start()
    {
        _data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        TimeSpentBox.SetText(SecToMinSec(_data.TotalTime));
        DistanceOnRoute.SetText(ConvertMetersToKilometers(_data.TotalDistance).ToString("F1") + "km");

    }
    
    private float ConvertMetersToKilometers(float distanceInMeters)
    {
        return distanceInMeters / 1000f;
    }
    
    string SecToMinSec(float input)
    {
        float min = Mathf.Floor(input / 60);
        float sec = input % 60;
        return min.ToString("0#") + ":" + sec.ToString("0#");
    }
}
