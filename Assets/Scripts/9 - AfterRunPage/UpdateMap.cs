using System;
using Navigation;
using UnityEngine;
using UnityEngine.UI;
using RequestsImage;

public class UpdateMap : MonoBehaviour
{
    private string Website = "http://klintoe.pythonanywhere.com/DrawMap/";
    public StoredData data;
    private RouteNodeList routeNodeList;
    [SerializeField]private GameObject objectToActivate;
    
    // Start is called before the first frame update
    async void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        routeNodeList = new RouteNodeList(data.Route);
        
        //"http://klintoe.pythonanywhere.com/DrawMap/57.01802465922206,9.990545365623875,57.027006281564795,9.98802408927767,57.018240757560264,9.992959353816799"


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
        //Debug.Log(CoordList);
        
        Sprite mapImage = await ImageRequester.GetImageFromUrl(Website + CoordList + "/" + POICoordList + "/" + homecoord + "/" + startcoord);
        
        //Debug.Log(Website + CoordList);
        data.MapImg = mapImage;
        
        objectToActivate.SetActive(true);
        
    }

    // Update is called once per frame
}
