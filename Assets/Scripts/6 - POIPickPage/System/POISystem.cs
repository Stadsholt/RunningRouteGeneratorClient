using UnityEngine;
using RequestsUtil;
using FilteringSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



public class POISystem : MonoBehaviour
{

    public POIInfo PoiData;
    public POIFilterSystem POImanager;
    [SerializeField]private GameObject generateRoute;



    [SerializeField]private GameObject[] objectToActivate;

    [SerializeField]private GameObject objectToDeactivate;
        
    [SerializeField]private GameObject NextPageButton;
    
    
    public StoredData data;

    async void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();

        PoiData = await Requests.GetPois(data.StartCoords.x, data.StartCoords.y, data.SelectedFilterData);
        POImanager = new POIFilterSystem(PoiData);

        // Turns off loading screen
        //objectToDeactivate.SetActive(false);

        foreach (var obj in objectToActivate)
        {
            obj.SetActive(true);
        }
        //Route routeData = (await Requests.GetRoute(57.01811, 9.992502, "driving-car", PoiData)).data;

        //Debug.Log(routeData.features[0].geometry.coordinates[0][0]);

    }

    // Update is called once per frame


    public async void ButtonClicked()
    {
        Debug.Log("A man has fallen into the river in lego city");
        var POIS = POImanager.GetSelectedPois(POImanager.PoiFilter);

        if(POIS.data.Count >= 1)
        {
            // Add home to POIList
            POIS.data.Add(new[] {"Home", "HomeID", data.HomeCoords.y.ToString(), data.HomeCoords.x.ToString()});

        
            Debug.Log("Images to use: " + POImanager.Images.Length);
        

            var Route = await Requests.GetRoute(data.StartCoords.x, data.StartCoords.y,
                data.DrivingProfile, POIS);
            data.Images = POImanager.Images;
            data.Route = Route;
            data.SelectedPointsOfInterests = POIS;

            SceneManager.LoadScene("7 - RouteConfirmPage");
        }
    }
}





