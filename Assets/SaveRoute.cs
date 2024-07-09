using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using RequestsUtil;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
using TMPro;

public class SaveRoute : MonoBehaviour
{
    StoredData data;
    
    [SerializeField] private TMP_Text Textbox;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SaveRouteToHistory()
    {
        string mainFolder = CreateMainFolder();
        string imgFolder = CreateFolder(mainFolder, "Images");
        string mapFolder = CreateFolder(mainFolder, "Map");
        string routeFolder = CreateFolder(mainFolder, "Route");
        string routeStatsFolder = CreateFolder(mainFolder, "RouteStats");
        string POIFolder = CreateFolder(mainFolder, "POIs");
        
        FetchImages(imgFolder);
        FetchMap(mapFolder);
        FetchRoute(routeFolder);
        FetchRouteStats(routeStatsFolder);
        FetchPOI(POIFolder);
        
        SceneManager.LoadScene("1 - MainPage");
    }
    string CreateMainFolder()
    {
        const string folderName = "Hist";
        int folderIndex = 0;
        string folderPath = Path.Combine(Application.persistentDataPath, folderName + folderIndex);

        while (Directory.Exists(folderPath))
        {
            folderIndex++;
            folderPath = Path.Combine(Application.persistentDataPath, folderName + folderIndex);
            //folderName = "hist" + folderIndex.ToString();
        }
        
        Directory.CreateDirectory(folderPath);
        return folderPath;
    }
    
    
    string CreateFolder(string mainFolder, string name)
    {
        string folderPath = Path.Combine(mainFolder, name);
        Directory.CreateDirectory(folderPath);
        return folderPath;
    }
    
    void FetchImages(string imgPath)
    {
        const string imgName = "POI";
        for (var i = 0; i < data.Images.Length; i++)
        {
            var name = imgName + i;
            var img = data.Images[i];
            byte[] imgBytes = img.texture.EncodeToPNG();
            File.WriteAllBytes(Path.Combine(imgPath, name+".png"), imgBytes);
        }
    }
    
    void FetchMap(string mapPath)
    {
        const string imgName = "Map.png";
        var map = data.MapImg;
        byte[] mapBytes = map.texture.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(mapPath, imgName), mapBytes);
    }

    void FetchPOI(string POIPath)
    {
        const string routeName = "POI.json";
        string POIData = JsonConvert.SerializeObject(data.SelectedPointsOfInterests);
        File.WriteAllText(Path.Combine(POIPath, routeName),POIData);
    }
    
    void FetchRoute(string routePath)
    {
        const string routeName = "Route.json";
        string routeData = JsonConvert.SerializeObject(data.Route);
        File.WriteAllText(Path.Combine(routePath, routeName),routeData);
    }

    void FetchRouteStats(string routeStatsPath)
    {
        const string routeName = "RouteStats.txt";
        
        string TotalDistance = (data.Route.data.features[0].properties.summary.distance / 1000).ToString("0.0") + " km";
        float TimeFloat = (float)data.Route.data.features[0].properties.summary.duration;
        string TotalTime = SecToMinSec(TimeFloat) + " min";
        string RouteRating = data.RouteRating.ToString();
        string CurrentTime = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        string StartCoordX = data.StartCoords.x.ToString();
        string StartCoordY = data.StartCoords.y.ToString();
        string HomeCoordX = data.HomeCoords.x.ToString();
        string HomeCoordY = data.HomeCoords.y.ToString();


        string RouteStats = $"{TotalDistance},{TotalTime},{RouteRating},{CurrentTime},{StartCoordX},{StartCoordY},{HomeCoordX},{HomeCoordY}";
        
        File.WriteAllText(Path.Combine(routeStatsPath, routeName),RouteStats);
    }
    
    string SecToMinSec(float input)
    {
        int minutes = Mathf.FloorToInt(input / 60f);
        return string.Format("{0:0}", minutes);
    }

}
