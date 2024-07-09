using UnityEngine;
using RequestsUtil;
using System.Collections.Generic;
using System;

public class StoredData : MonoBehaviour
{
    private static GameObject sampleInstance;
    private void Awake()
    {
        if (sampleInstance != null)
            Destroy(this);
        else{
            DontDestroyOnLoad(this);
            sampleInstance = gameObject;
        }
        Username = SystemInfo.deviceUniqueIdentifier;
    }

    public string DrivingProfile {get; set;}
    [SerializeField] public Sprite[] Images {get; set;}

    public RouteData Route;
    
    public string[] SelectedFilterData;

    public POIInfo SelectedPointsOfInterests; //Users point of interests that is selected but can be changed if the user want to see something new

    public Vector2 StartCoords;
    
    public Vector2 HomeCoords = new Vector2(0,0);

    public float TotalDistance = 0f;
    
    public float TotalTime = 0f;
    
    public Sprite MapImg;
    
    public string Username;
    
    public int RouteRating;
    
    public bool ExploreMode = true;
    
    public bool BikeMode = false;

    public List<string[]> POIPassed = new List<string[]>(); //user can change route while not losing the passed Pois

    public List<Sprite> CurrentSprites = new List<Sprite>();


}

[Serializable]
public class RouteHistoryData {
    public int rating;
    public string date;
    public float distance;
    public float duration;
    public string query;
    public byte[] map;
    public byte[] poi1;
    public byte[] poi2;
    public POIInfo selectedPOIs;
}