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

public class SendDataToDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    StoredData data;
    [SerializeField] private Slider MySlider;
    [SerializeField] private TMP_Text Textbox;
    [SerializeField] private Image MySprite;

    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
    }
    public async void OnClick()
    {
        data.RouteRating = (int)MySlider.value;
        List<string> CatIDs = new List<string>();
        
        List<string[]> selectedPointsOfInterests = data.SelectedPointsOfInterests.data;

        string[] poiToRemove = new[] { "Home", "HomeID", data.HomeCoords.y.ToString(), data.HomeCoords.x.ToString() };

        selectedPointsOfInterests.RemoveAll(poi => poi.SequenceEqual(poiToRemove));

        data.SelectedPointsOfInterests.data = selectedPointsOfInterests;
        
        // Find all subcategory IDs
        foreach (var IDs in data.SelectedPointsOfInterests.data)
        {
            CatIDs.Add(IDs[1]);
        }
        
        // Send them and their rating to the API
        if (CatIDs.Count >= 1)
        {
            await Requests.PostToDatabase(data.Username, CatIDs.ToArray(), data.RouteRating);
        }
    }
}