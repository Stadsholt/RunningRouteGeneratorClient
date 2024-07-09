using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RequestsUtil;
using UnityEngine.SceneManagement;

public class RouteHistoryCard : MonoBehaviour
{
    StoredData data;
    public ShowRating ratingScript;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI distanceValue;
    public TextMeshProUGUI durationValue;
    public bool DeletePOI2 = false;
    public Image map;
    public Image poi1;
    public Image poi2;
    public GameObject POI2Obj;
    public string Distance;
    public string Time;
    public int Rating;
    public string Date;
    public Vector2 StartCoord;
    public Vector2 HomeCoord;
    public RouteData Route;
    
    public POIInfo POIs;
    
    public Sprite[] images;
    
   
    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        distanceValue.SetText(Distance);
        durationValue.SetText(Time);
        ratingScript.rating = Rating;
        dateText.SetText(Date);
        if (DeletePOI2 == true)
        {
            Destroy(POI2Obj);
        }
    }
    

    public void StartSavedRoute()
    {
        data.MapImg = map.sprite;
        data.Route = Route;
        data.Images = images;
        data.StartCoords = StartCoord;
        data.HomeCoords = HomeCoord;
        data.SelectedPointsOfInterests = POIs;
        
        string[] HomePOI = { "Home", "HomeID", data.HomeCoords.y.ToString(), data.HomeCoords.x.ToString() };
        data.SelectedPointsOfInterests.data.Add(HomePOI);
        
        SceneManager.LoadScene("8 - ArrowPage");
    }
}
