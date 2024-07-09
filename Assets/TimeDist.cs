using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDist : MonoBehaviour
{
    // Start is called before the first frame update
    private StoredData datas;

    [SerializeField]private TMP_Text TimeSpentBox;
    [SerializeField]private TMP_Text DistanceOnRoute;
    
    void Start()
    {
        datas = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        DistanceOnRoute.SetText((datas.Route.data.features[0].properties.summary.distance / 1000).ToString("0.0") + " km");
        float TimeFloat = (float)datas.Route.data.features[0].properties.summary.duration;
        TimeSpentBox.SetText(Conversion.SecToMinSec(TimeFloat) + " min");
    }

}
public class Conversion {
    public static string SecToMinSec(float input)
    {
        int minutes = Mathf.FloorToInt(input / 60f);
        int seconds = Mathf.FloorToInt(input % 60f);
        
        if(input == 0)
        {
            return "--:--";
        }
        return string.Format("{0:0}", minutes);
    }

}
