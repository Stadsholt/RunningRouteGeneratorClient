using System;
using System.Collections;
using System.Collections.Generic;
using RequestsUtil;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] ObjectsToActivate;
    [SerializeField] private GameObject[] ObjectsToDeActivate;
    
    [SerializeField] private GameObject[] ObjectsToActivate2;
    [SerializeField] private GameObject[] ObjectsToDeActivate2;
    
    [SerializeField]private GameObject[] LoadingActi;
    [SerializeField] private Sprite HomeSprite;

    public StoredData data;
    Sprite[] Images = Array.Empty<Sprite>();

    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
    }
    
    public void ShowMenu()
    {
        foreach (var t in ObjectsToDeActivate)
        {
            t.SetActive(true);
        }
        
    }
    
    
    public void EditRoute()
    {
        data.StartCoords = new Vector2(Input.location.lastData.latitude,Input.location.lastData.longitude);
        SceneManager.LoadScene("5 - CategoryPage");
    }
    
    public async void GoHome()
    {
        data.StartCoords = new Vector2(Input.location.lastData.latitude,Input.location.lastData.longitude);
        foreach (var obj in LoadingActi)
        {
            obj.SetActive(true);
        }
        
        data.Images = Images;
        List<String[]> POIS = new List<string[]>();
        
        // Add home to POIList
        POIS.Add(new[] {"Home", "HomeID", data.HomeCoords.y.ToString(), data.HomeCoords.x.ToString()});

        // create route
        var Route = await Requests.GetRoute(data.StartCoords.x, data.StartCoords.y,data.DrivingProfile, new POIInfo(POIS));
        
        // save data to stored data
        data.Route = Route;
        data.SelectedPointsOfInterests = new POIInfo(POIS);
        
        
        SceneManager.LoadScene("8 - ArrowPage");
    }

    public void MainMenu()
    {
        foreach (var t in ObjectsToDeActivate2)
        {
            t.SetActive(true);
        }
    }
    
    public void HideMenu()
    {
        foreach (var t in ObjectsToDeActivate)
        {
            t.SetActive(false);
        }
        foreach (var t in ObjectsToDeActivate2)
        {
            t.SetActive(false);
        }
    }
    
    public void Yes()
    {
        SceneManager.LoadScene("1 - MainPage");
    }
    
    public void No()
    {
        foreach (var t in ObjectsToDeActivate2)
        {
            t.SetActive(false);
        }
    }
}
