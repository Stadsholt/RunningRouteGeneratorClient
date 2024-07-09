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
using UnityEngine.UI;

public class RouteClick : MonoBehaviour
{

    [Header("This route")] 
    [SerializeField] private Image MapBox;
    [SerializeField] private TextMeshProUGUI TitleBox;
    [SerializeField] private TextMeshProUGUI DistanceBox;
    [SerializeField] private TextMeshProUGUI TimeBox;
    [SerializeField] private TextMeshProUGUI RatingBox;
    [SerializeField] private Image POI1Box;
    
    
    [Header("Big Map")] 
    [SerializeField] private GameObject Page;
    [SerializeField] private GameObject BigMap;
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject Distance;
    [SerializeField] private GameObject Time;
    [SerializeField] private GameObject Rating;
    [SerializeField] private GameObject POI1;
    // Start is called before the first frame update
    void Start()
    {
        Page = GameObject.FindWithTag("BigMap");
        BigMap = Page.transform.Find("BigMap").gameObject;
        Map = BigMap.transform.Find("Map").gameObject;
        Title = BigMap.transform.Find("Title").gameObject;
        Title = Title.transform.Find("Text").gameObject;
        
        Distance = BigMap.transform.Find("Distance").gameObject;
        Distance = Distance.transform.Find("Text").gameObject;
        
        Time = BigMap.transform.Find("Time").gameObject;
        Time = Time.transform.Find("Text").gameObject;
        
        Rating = BigMap.transform.Find("Rating").gameObject;
        Rating =  Rating.transform.Find("Text").gameObject;
        
        POI1 = BigMap.transform.Find("POI1").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RouteButtonClick()
    {
        BigMap.SetActive(true);
        Map.GetComponent<Image>().sprite = MapBox.sprite;
        POI1.GetComponent<Image>().sprite = POI1Box.sprite;
        Title.GetComponent<TextMeshProUGUI>().text = TitleBox.text;
        Distance.GetComponent<TextMeshProUGUI>().text = DistanceBox.text;
        Time.GetComponent<TextMeshProUGUI>().text = TimeBox.text;
        Rating.GetComponent<TextMeshProUGUI>().text = "Rating: " + RatingBox.text;
    }
}
