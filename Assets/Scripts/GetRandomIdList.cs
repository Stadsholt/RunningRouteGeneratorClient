using System;
using System.Collections;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using RequestsImage;
using UnityEngine;
using RequestsUtil;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetRandomIdList : MonoBehaviour
{
    
    public StoredData data;
    [SerializeField] private int AmountOfPOI = 3;
    [SerializeField] private GameObject[] objectToActivate;
    [SerializeField] private SimpleScrollSnap MySnap;
    [SerializeField] private Slider sliderValue;
    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
    }
    
    
    public async void ButtonClicked()
    {
        foreach (var obj in objectToActivate)
        {
            obj.SetActive(true);
        }
        
        // Get randomIdlist
        var IDList = await Requests.GetRandomIdList(data.Username, data.StartCoords.x, data.StartCoords.y);
        // AmountOfPOI = MySnap.CenteredPanel + 1;
        AmountOfPOI = (int) sliderValue.value;

        // find POIs
        var PoiData = await Requests.GetPois(data.StartCoords.x, data.StartCoords.y,  IDList.data.ToArray());
        int Counter = 0;
        List<string> PickedCatIDs = new List<string>();
        List<Sprite> POIImages = new List<Sprite>();
        List<String[]> POIS = new List<string[]>();

        Debug.Log("Found random POIs nearby: " + PoiData.data.Count);
        
        
        System.Random rng = new System.Random();
        int n = PoiData.data.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            (PoiData.data[k], PoiData.data[n]) = (PoiData.data[n], PoiData.data[k]);
        }
        
        
        
        // find images
        foreach (var POI in PoiData.data)
        {
            if (Counter >= AmountOfPOI)
            {
                break;
            }
            
            var googlePlacesImageURL = new GooglePlacesImageURL();
            Sprite image;

            if (!PickedCatIDs.Contains(POI[1]))
            {
                try
                {
                    image = await googlePlacesImageURL.NearbySearch(POI[0], POI[3], POI[2]);
                    POIImages.Add(image);
                    PickedCatIDs.Add(POI[1]);
                    POIS.Add(POI);
                    Counter++;
                    Debug.Log("Found random POI image");
                }
                catch (Exception)
                {
                    Debug.Log("Failed to load random image");
                }
            }
        }
        Debug.Log("Length of found POIImages " + POIImages.Count);
        Debug.Log("POIS length: " + POIS.Count);

        if (POIS.Count >= 1)
        {
            // Add home to POIList
            POIS.Add(new[] {"Home", "HomeID", data.HomeCoords.y.ToString(), data.HomeCoords.x.ToString()});
        
        
            // create route
            var Route = await Requests.GetRoute(data.StartCoords.x, data.StartCoords.y,data.DrivingProfile, new POIInfo(POIS));
        
            // save data to stored data
            data.Images = POIImages.ToArray();
            data.Route = Route;
            data.SelectedPointsOfInterests = new POIInfo(POIS);

            SceneManager.LoadScene("7 - RouteConfirmPage");
        }
        else
        {
            SceneManager.LoadScene("4 - PrefPage");
        }
    }
        
        
        
        
        
        
        
    


}
