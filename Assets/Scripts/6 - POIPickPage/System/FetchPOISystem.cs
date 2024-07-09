using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
using System.Threading.Tasks;
using RequestsUtil;
using System.Linq;
using FilteringSystem;
using UnityEngine.SceneManagement;

public class FetchPOISystem : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<string> POIList;
    [SerializeField] private List<GameObject> POIBox;

    [SerializeField] private GameObject[] POIBoxAfterDeletion;

    [SerializeField] private GameObject LoadingPage;
    
    public int AmountOfPOI;

    bool Lock1 = false; 

    [Header("POISystem")]
    public POISystem POISystem;

    [Header("Objects")]
    [SerializeField] private GameObject Prefab;

    [SerializeField] private GameObject InstantiateTarget;

    [Header("Scene to load in case of failure")]
    public string SceneName = "5 - CategoryPage";


    // Start is called before the first frame update
    void Start()
    {
        POISystem = GameObject.Find("POISystem").GetComponent<POISystem>();

            

        //POIList = POISystem.filterManager.Get(POISystem.filterManager.CategoryFilters);
        AmountOfPOI = 0;
        
        // Create category text boxes
        
        for (int i = 0; i <  POISystem.PoiData.data.Count; i++)
        {
            GameObject obj = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity, InstantiateTarget.transform) as GameObject;
            POIBox.Add(obj);
            AmountOfPOI++;
            if (AmountOfPOI >= 30)
            {
                break;
            }
        }




        // Set all the text and make boxes visible
        for (int i = 0; i < POISystem.PoiData.data.Count; i++)
        {
            POIBox[i].GetComponentInChildren<TextMeshProUGUI>().text = POISystem.PoiData.data[i][0];


            POIBox[i].GetComponentInChildren<getimg>().FetchPoi = this;
            POIBox[i].GetComponentInChildren<getimg>().Name = POISystem.PoiData.data[i][0];
            POIBox[i].GetComponentInChildren<getimg>().Lat = POISystem.PoiData.data[i][2];
            POIBox[i].GetComponentInChildren<getimg>().Long = POISystem.PoiData.data[i][3];

            POIBox[i].GetComponentInChildren<POIButton>().Subcategory = POISystem.PoiData.data[i][1];
            POIBox[i].GetComponentInChildren<POIButton>().POIName = POISystem.PoiData.data[i][0]; 
            POIBox[i].GetComponentInChildren<POIButton>().POILat = POISystem.PoiData.data[i][3];
            POIBox[i].GetComponentInChildren<POIButton>().POILon = POISystem.PoiData.data[i][2];



        }

        //POISystem.POImanager.PoiFilter


        // Allow Update to check if any objects survived destruction
        //FinishedLoading();

        Lock1 = false; 

    }
    void Update()
    {
        // runs until it has checked all images, then disables itself
        if (Lock1 == false)
        {
            // remove missing objects from list
            POIBox = POIBox.Where(item => item != null).ToList();

            // check if they have all loaded their images
            bool Alltrue = true;
            for (int i = 0; i < POIBox.Count; i++)
            {
                if (POIBox[i].GetComponentInChildren<getimg>().Loaded == false)
                {
                    Alltrue = false;
                    Lock1 = false;
                    Debug.Log("Found POI that hasn't loaded yet, continuing to load");
                    break;
                }
            }
            // If all have loaded
            if (Alltrue == true)
            {
                Lock1 = true;
                // then check if the list is empty and go back one page
                if (POIBox.Count == 0)
                {
                Debug.Log("No POI's could be found with these categories, going back to preference page");
                SceneManager.LoadScene(SceneName);
                }
                else  // otherwise disable loading screen and show the page
                {
                    Debug.Log("All POI have loaded");
                    LoadingPage.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame

}
