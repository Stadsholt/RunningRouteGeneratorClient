using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using FilteringSystem;
using RequestsUtil;
using System;
using TMPro;
using Unity.VectorGraphics;

public class POIButton : MonoBehaviour
{
    [SerializeField] private Transform POIBox;
    [SerializeField] private TextMeshProUGUI POINameBox;
    public string POIName;
    public string Subcategory;
    public string POILat;
    public string POILon;
    [SerializeField] private GameObject Filter;
    [SerializeField] private bool Bool1 = false;

    [SerializeField] private SVGImage spriteRenderer;
    
    [SerializeField] private POISystem POISystem;
    [SerializeField] private POIFilterSystem POImanager;

    [SerializeField] TextMeshProUGUI estRouteDistanceText;
    [SerializeField] StoredData data;
    [SerializeField] TextMeshProUGUI DistanceToUserText;
    [SerializeField] TextMeshProUGUI SubcategoryText;
    
    [SerializeField] GameObject POIZoom;
    
    [SerializeField] GameObject ZoomPic;
    //[SerializeField] private GameObject ZoomPic;
    // Start is called before the first frame update
    void Start()
    {
        POIZoom = GameObject.FindGameObjectWithTag("POIZoom");
        POISystem = FindObjectOfType<POISystem>();
        POImanager = POISystem.POImanager;
        Bool1 = false;
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        estRouteDistanceText = GameObject.FindWithTag("EstDistanceNum").GetComponent<TextMeshProUGUI>();
        float lat = Convert.ToSingle(POILat);
        float lon = Convert.ToSingle(POILon);
        DistanceToUserText.text =
            (GetDistanceBetweenGeoCoordinates(data.StartCoords.x, data.StartCoords.y, lat, lon) / 1000).ToString("N1") +
            " km";
        SubcategoryText.text = Subcategory;
        POINameBox.text = POIName;
    }
    public void Click()
    {
        Bool1 = !Bool1;
        if (Bool1)
        {
            ON();
        }
        else
        {
            OFF();
        }
        EstimateRouteDistance();
    }

    public void ON()
    {
        Filter.SetActive(true);
        foreach (var t in POISystem.POImanager.PoiFilter.Where(t => t.Name == POIName && t.Lat == POILat))
        {
            t.IsSelected = true; 
            t.Image = spriteRenderer.sprite;
        }
    }

    public void OFF()
    {
        Filter.SetActive(false);
        foreach (var t in POISystem.POImanager.PoiFilter.Where(t => t.Name == POIName))
        {
            t.IsSelected = false;
        }
    }

    float GetDistanceBetweenGeoCoordinates(float lat1, float lon1, float lat2, float lon2)
    {
        float dLat = Mathf.Deg2Rad * (lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (lon2 - lon1);

        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        float distance = 6371000 * c;
        return distance;
    }

    public void EstimateRouteDistance()
    {
        POIInfo POIList = POImanager.GetSelectedPois(POImanager.PoiFilter);
        float startLat = data.StartCoords.x;
        float startLon = data.StartCoords.y;
        Debug.Log("POIs selected: " + POIList.data.Count);
        float previousLat = startLat;
        float previousLon = startLon;

        float estimatedDistance = 0;
        for (int i = 0; i < POIList.data.Count; i++)
        {
            float lat = Convert.ToSingle(POIList.data[i][3]);
            float lon = Convert.ToSingle(POIList.data[i][2]);
            Debug.Log(lat + " " + lon);

            estimatedDistance += GetDistanceBetweenGeoCoordinates(previousLat, previousLon, lat, lon);
            previousLat = lat;
            previousLon = lon;
        }
        estimatedDistance += GetDistanceBetweenGeoCoordinates(previousLat, previousLon, startLat, startLon);
        estRouteDistanceText.text = (1.2 * estimatedDistance/1000).ToString("N1");
    }

    public void ZoomButton()
    {
        // Set zoom picture to this when clicked
        // Enable the children of the POIZoom object
        POIZoom.transform.Find("Image").gameObject.SetActive(true);
        POIZoom.transform.Find("ExitButton").gameObject.SetActive(true);
        ZoomPic = POIZoom.transform.Find("ZoomPic").gameObject;    
        // Set zoom picture to this when clicked
        ZoomPic.GetComponentInChildren<SVGImage>().sprite = spriteRenderer.sprite;
        ZoomPic.SetActive(true);
    }

}
