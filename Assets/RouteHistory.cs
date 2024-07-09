using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
using System.Threading.Tasks;
using RequestsUtil;
using System.Linq;
using FilteringSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RouteHistory : MonoBehaviour
{
    [SerializeField] private TMP_Text Textbox;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject InstantiateTarget;
    [SerializeField] private List<GameObject> History;
// Start is called before the first frame update
    void Start()
    {
        int Folders = FindAmountOfFolders();

        for (int i = 0; i < Folders; i++)
        {
            GameObject obj = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity, InstantiateTarget.transform) as GameObject;
            History.Add(obj);

            string folderName = "Hist"+i;
            string historyFolder = Path.Combine(Application.persistentDataPath,folderName);
            List<Sprite> images = new List<Sprite>();
            
            string imgDirectory = Path.Combine(historyFolder, "Images");
            string routeStatsDirectory = Path.Combine(historyFolder, "RouteStats");
            string routeDirectory = Path.Combine(historyFolder, "Route");
            string mapDirectory = Path.Combine(historyFolder, "Map");
            string POIsDirectory = Path.Combine(historyFolder, "POIs");
            
            for (int j = 0; j < FindAmountOfElements(imgDirectory); j++)
            {
                images.Add(FetchSprite(Path.Combine(imgDirectory, "POI"+j+".png")));
            }
            
            obj.GetComponent<RouteHistoryCard>().poi1.sprite = images[0];
            if (images.Count > 1)
            {
                obj.GetComponent<RouteHistoryCard>().poi2.sprite = images[1];
            }
            else
            {
                obj.GetComponent<RouteHistoryCard>().DeletePOI2 = true;
            }
            
            obj.GetComponent<RouteHistoryCard>().Route = GetRoute(Path.Combine(routeDirectory, "Route.json"));
            obj.GetComponent<RouteHistoryCard>().POIs = GetPOis(Path.Combine(POIsDirectory, "POI.json"));
            obj.GetComponent<RouteHistoryCard>().images = images.ToArray();
            obj.GetComponent<RouteHistoryCard>().map.sprite = FetchSprite(Path.Combine(mapDirectory, "Map.png"));
            string[] stats = GetStats(Path.Combine(routeStatsDirectory, "RouteStats.txt"));
            obj.GetComponent<RouteHistoryCard>().Distance = stats[0];
            obj.GetComponent<RouteHistoryCard>().Time = stats[1];
            obj.GetComponent<RouteHistoryCard>().Rating = Int32.Parse(stats[2]);
            obj.GetComponent<RouteHistoryCard>().Date = stats[3];

            string StartX = stats[4];
            string StartY = stats[5];
            
            obj.GetComponent<RouteHistoryCard>().StartCoord = new Vector2(float.Parse(StartX),float.Parse(StartY));
           
            string HomeX = stats[6];
            string HomeY = stats[7];
            obj.GetComponent<RouteHistoryCard>().HomeCoord = new Vector2(float.Parse(HomeX),float.Parse(HomeY));
        }
    }
    
    POIInfo GetPOis(string path)
    {
        StreamReader reader = new StreamReader(path);
        string JSONData = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<POIInfo>(JSONData);
    }
    
    RouteData GetRoute(string path)
    {
        StreamReader reader = new StreamReader(path);
        string JSONData = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<RouteData>(JSONData);
    }
    
    
    //_____Other versions____
    
    //POIInfo GetPOis(string path)
    //{
    //string JSONData = File.ReadAllText(path);
    //string jsonString = "{\"data\":" + JSONData + " }";
    // jsonString = System.Text.RegularExpressions.Regex.Unescape(jsonString);
    // return JsonConvert.DeserializeObject<POIInfo>(jsonString);
    //}
    
    //RouteData GetRoute(string path)
    //{
    //   string JSONData = File.ReadAllText(path);
    //   string jsonString = "{\"data\":" + JSONData + " }";
    //    jsonString = System.Text.RegularExpressions.Regex.Unescape(jsonString);
    //    return JsonConvert.DeserializeObject<RouteData>(jsonString);
    // }
 
    
    
    
    string[] GetStats(string path)
    {
        StreamReader reader = new StreamReader(path);
        return reader.ReadToEnd().Split(',');
    }
    
    
    Sprite FetchSprite(string path)
    {
        byte[] imageBytes = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(imageBytes);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
    
    int FindAmountOfElements(string path)
    {
        return Directory.GetFiles(path).Length;
    }

    int FindAmountOfFolders()
    {
        return Directory.GetDirectories(Application.persistentDataPath).Length;
    }
}
