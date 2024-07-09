using System;
using Navigation;
using UnityEngine;
using UnityEngine.UI;
using RequestsImage;
using TMPro;

public class GetMapImage : MonoBehaviour
{
    private string Website = "Insert-your-map-API-url/DrawMap/"; // Insert the map API url here, include /DrawMap/ after the url
    public StoredData data;
    private RouteNodeList routeNodeList;
    [SerializeField]private GameObject[] objectToDeactivate;
    [SerializeField] private TMP_Text Textbox;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        routeNodeList = new RouteNodeList(data.Route);
        
        var spriteRenderer = gameObject.GetComponent<Image>();

        string CoordList = String.Empty;
        string POICoordList = String.Empty;

        foreach (var coordinates in routeNodeList.TurnNodes)
        {
           
            CoordList += coordinates.coordinates.x.ToString();
            CoordList += ",";
            CoordList += coordinates.coordinates.y.ToString();
            CoordList += ",";
        }
        foreach(var poiCoords in data.SelectedPointsOfInterests.data)
        {
            POICoordList += poiCoords[3];
            POICoordList += ",";
            POICoordList += poiCoords[2];
            POICoordList += ",";
        }

            string homecoord = data.HomeCoords.x + "," + data.HomeCoords.y;
            string startcoord = data.StartCoords.x + "," + data.StartCoords.y;
            
            CoordList = CoordList.Remove(CoordList.Length - 1);
            POICoordList = POICoordList.Remove(POICoordList.Length-1);

            Sprite mapImage = await ImageRequester.GetImageFromUrl(Website + CoordList + "/" + POICoordList + "/" + homecoord + "/" + startcoord);
            spriteRenderer.sprite = mapImage;
            spriteRenderer.color = Color.white;
            data.MapImg = mapImage;
 
       
        
      
        foreach (var obj in objectToDeactivate)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
}
