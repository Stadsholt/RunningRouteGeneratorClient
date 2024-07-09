using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;


public class SetHome : MonoBehaviour
{
    private StoredData datas;
    [SerializeField] private TMP_Text Textbox;
    [SerializeField] private float HideTime = 3;
    [SerializeField] private TMP_Text HomeLocationText;
    
    // Start is called before the first frame update
    void Start()
    {
        datas = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();

        // Check if "Home" key exists in PlayerPrefs
        if (PlayerPrefs.HasKey("Home_x") && PlayerPrefs.HasKey("Home_y"))
        {
            // Get the value of "Home" key from PlayerPrefs
            Vector2 homeLocation = new Vector2(PlayerPrefs.GetFloat("Home_x"), PlayerPrefs.GetFloat("Home_y"));

            // Set the value of datas.HomeCoords to the saved home location
            datas.HomeCoords = homeLocation;
            
            // display that it found home data
            Textbox.SetText("Loaded saved home!");
            
            // Set the text of HomeLocationText to the saved home location
            HomeLocationText.SetText("Current home: " + homeLocation.ToString());
            StartCoroutine(HideInstructions(HideTime));
        }
        else
        {
            // Call SetHomeButton() to set the home location if "Home" key doesn't exist
            SetHomeButton();
        }
    }
    
    
    public void SetHomeButton()
    {
        // Get the current location
        Vector2 currentLocation = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);

        // Set the value of datas.HomeCoords to the current location
        datas.HomeCoords = currentLocation;

        // Set the value of "Home" key in PlayerPrefs to the current location
        PlayerPrefs.SetFloat("Home_x", currentLocation.x);
        PlayerPrefs.SetFloat("Home_y", currentLocation.y);

        // Save PlayerPrefs
        PlayerPrefs.Save();

        // Set the text of HomeLocationText to the location set by SetHomeButton()
        HomeLocationText.SetText("Current home: " + datas.HomeCoords.ToString());
        
        Textbox.SetText("Home set!");
        StartCoroutine(HideInstructions(HideTime));
        Debug.Log("Home set");
    }
    
    
    IEnumerator HideInstructions(float HideTime)
    {
        yield return new WaitForSeconds(HideTime);
        Textbox.SetText("Set current location as Home");
    }
    
    public void NextPage()
    {
        SceneManager.LoadScene("5 - CategoryPage");
    }
    
}
