using UnityEngine;

public class LocationSystem : MonoBehaviour
{ 
    public Vector2 locationData {get; private set;}
    //option for user to pick a random location function that will maybe be added later
    
    [Header("Use test location?")]
    
    [SerializeField] private bool UsingTestLocation = false;
    
    
    [Header("Pick preset test location or write custom location")]
    [SerializeField] private bool Grøndlandstorvet = false;
    
    [SerializeField] private bool AalborgØstAtletikStadion = false;
    
    [SerializeField] private bool København = false;
    
    [SerializeField] private bool OmkringKildeparken = false;
    
    
    [Header("Custom location - Deselect all presets above")]
    [SerializeField] private bool CustomLocationBool = false;
    [SerializeField] private Vector2 CustomLocation;

    [SerializeField] private GameObject NextButton;
    private GPSToggle gpsToggle;

    public StoredData data;

    void Start()
    {
        gpsToggle = FindObjectOfType<GPSToggle>();
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(gpsToggle.IsLocationReady())
        {
            if (UsingTestLocation == false)
            {
                locationData = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            }
            else
            {
                if (Grøndlandstorvet)
                {
                    locationData = new Vector2(57.0227f, 9.942534f);
                    Debug.Log("-------WARNING: USING TEST LOCATION: Grøndlandstorvet------- " +
                              "(Turn toggle off on 'LocationSystem' object in 'LocationSelectPage' scene before exporting to Android)");
                }
                if (AalborgØstAtletikStadion)
                {
                    locationData = new Vector2(57.024148903721105f, 10.004180429090182f);
                    Debug.Log("-------WARNING: USING TEST LOCATION: Aalborg Øst Atletik Stadion------- " +
                              "(Turn toggle off on 'LocationSystem' object in 'LocationSelectPage' scene before exporting to Android)");
                }
                if (København)
                {
                    Debug.Log("-------WARNING: USING TEST LOCATION: København------- " +
                              "(Turn toggle off on 'LocationSystem' object in 'LocationSelectPage' scene before exporting to Android)");
                    locationData = new Vector2(55.68514571729542f, 12.58206654884968f);
                }
                if (OmkringKildeparken)
                {
                    Debug.Log("-------WARNING: USING TEST LOCATION: Omkring Kildeparken------- " +
                              "(Turn toggle off on 'LocationSystem' object in 'LocationSelectPage' scene before exporting to Android)");
                    locationData = new Vector2(57.04502f, 9.913838f);
                }
                if (CustomLocationBool)
                {
                    Debug.Log("-------WARNING: USING TEST LOCATION------- " +
                              "(Turn toggle off on 'LocationSystem' object in 'LocationSelectPage' scene before exporting to Android)");
                    locationData = CustomLocation;
                }
                
                
            }

            data.StartCoords = locationData;
            //NextButton.SetActive(true);
            Destroy(this);
        }
    }
}
