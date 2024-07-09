using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using FilteringSystem;
using System.Threading.Tasks;

public class CategoryToggle : MonoBehaviour

{   [Header("Filter system")]
    [SerializeField] private CategoryFilterManager filterManager;
    [SerializeField] private FilterSystem FilterSystem;

    [Header("Category")]
    [SerializeField] private Transform CategoryTextBox;
    [SerializeField] private string CategoryText;
    [SerializeField] private string CategoryTextDisplay;
    [SerializeField] private int Toggle = 2;
    [SerializeField] private Button CategoryButton;
    [SerializeField] private GameObject CategoryOn;
    [SerializeField] private GameObject CategoryMiddle;
    [SerializeField] private GameObject CategoryOff;
    [SerializeField] private Color OnColor;    
    [SerializeField] private Color MiddleColor;

    [Header("SubCategory")]
    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject SubCategoryDone;
    [SerializeField] private Button SubCategoryDoneButton;
    [SerializeField] private GameObject SubCategoryMain;
    [SerializeField] private GameObject SubCategoryMenu;
    [SerializeField] private GameObject SubCategoryFilter;
    [SerializeField] private GameObject SubCategoryHeader;
    [SerializeField] private List<GameObject> SubCategoryTexts;
    [SerializeField] private List<string> SubCategoryList;
    [SerializeField] private List<string> SubCategoryIDList;
    [SerializeField] private List<GameObject> DeleteObj = new List<GameObject>();

    [Header("SubCategoryScalingMenu")]
    [SerializeField] private GameObject SubCategoryBackgroundToScale;
    [SerializeField] private float SubCategoryBackgroundScaleNumber = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        FilterSystem = FindObjectOfType<FilterSystem>();
        filterManager = FilterSystem.filterManager;

        // Wait for the FilterSystem to finish loading asynchronously
        Toggle = 2;
        
        //find all objects, even if disabled
        CategoryMiddle.SetActive(false);
        CategoryOn.SetActive(false);
        SubCategoryMain = GameObject.FindWithTag("SubCategoryMenu");
        SubCategoryMenu = SubCategoryMain.transform.Find("Menu").gameObject;
        SubCategoryFilter = SubCategoryMain.transform.Find("Filter").gameObject;
        SubCategoryHeader = SubCategoryMain.transform.Find("Header").gameObject;
        CategoryText = CategoryTextBox.GetComponentInChildren<TextMeshProUGUI>().text;
        CategoryTextDisplay = CategoryText;
        // SubCategoryBackgroundToScale  = SubCategoryBackground.transform.Find("Container").gameObject;
        
        // Find Done button
        var GenPage = GameObject.FindWithTag("PreferencesPage");
        SubCategoryDone = GenPage.transform.Find("Done button").gameObject;
        SubCategoryDoneButton = SubCategoryDone.GetComponentInChildren<Button>();
    
        
        
        SubCategoryBackgroundScaleNumber = 0;
        
        //check if the case is "leisure_and_entertainment", so we can make text shorter so it fits inside box. Its the only long category name

        
        switch (CategoryText)
        {
            case "sustenance":
                CategoryTextDisplay = "Food";
                break;
            case "leisure_and_entertainment":
                CategoryTextDisplay = "Entertainment";
                break;
            default:
                CategoryTextDisplay = UppercaseFirst(CategoryText);
                CategoryTextDisplay = CategoryTextDisplay.Replace("_", " ");
                break;
        }

        CategoryTextBox.GetComponentInChildren<TextMeshProUGUI>().text = CategoryTextDisplay;
        //SubCategoryBackground.GetComponentInChildren<TextMeshProUGUI>().text = CategoryTextDisplay;

        // Add subcategories to list
        foreach (var t in filterManager.CategoryFilters.Where(t => t.CategoryName == CategoryText))
        {
            SubCategoryList.Add(t.SubcategoryName);
            //Debug.Log("Added " + filterManager.CategoryFilters[i].SubcategoryName + " to list, with ID: " + filterManager.CategoryFilters[i].Id);
            SubCategoryIDList.Add(t.Id);

            if (SubCategoryList.Count != 0) continue;
            Debug.Log("Category " + CategoryTextDisplay + " is empty, deleting");
            Destroy(SubCategoryMain);
        } 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   void LoadColors()
    {
        switch (Toggle)
        {
            case 3:
            {
                CategoryMiddle.SetActive(true);
                CategoryOn.SetActive(false);
                CategoryOff.SetActive(false);
                ColorBlock cb = CategoryButton.colors;
                cb.normalColor = MiddleColor;
                CategoryButton.colors = cb;
                break;
            }
            case 2:
            {
                CategoryMiddle.SetActive(false);
                CategoryOn.SetActive(false);
                ColorBlock cb = CategoryButton.colors;
                cb.normalColor = OnColor;
                CategoryButton.colors = cb;
                break;
            }
            case 1:
                CategoryMiddle.SetActive(false);
                CategoryOn.SetActive(true);
                break;
        }
    }

    public void Click()
    {
        // Switch between the 3 stages
        switch (Toggle)
        {
        case 1:
            Toggle = 2;
            OFF();
            //Debug.Log("Clicked to 2");
            break;
        case 2:
            Toggle = 1;
            ON();
            //Debug.Log("Clicked to 1");
            break;
        case 3:
            Toggle = 2;
            LoadColors();
            OFF();
            //Debug.Log("Clicked to 2 from 3");
            break;
        default:
            break;
        }
}

    public void SubButton()
    {
        // Open the menu
        SubCategoryFilter.SetActive(true);
        SubCategoryMenu.SetActive(true);
        SubCategoryDone.SetActive(true);
        SubCategoryHeader.SetActive(true);
        SubCategoryDoneButton.onClick.AddListener(DoneButton);

        SubCategoryTexts.Clear();

        // instantiate subcategories

        foreach (var obj in from t in filterManager.CategoryFilters where t.CategoryName == CategoryText select Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity, SubCategoryMenu.transform) as GameObject)
        {
            SubCategoryTexts.Add(obj);
        } 

        SubCategoryMenu.GetComponentInChildren<TextMeshProUGUI>().text = CategoryTextDisplay;

        // Set all the text
        for (int i = 0; i < SubCategoryTexts.Count; i++)
        { 
            SubCategoryTexts[i].GetComponentInChildren<TextMeshProUGUI>().text = SubCategoryList[i];
            SubCategoryTexts[i].GetComponentInChildren<Text>().text = SubCategoryIDList[i];
        }

        
        SubCategoryBackgroundScaleNumber = SubCategoryTexts.Count;
        // If amount of subcategory boxes is odd, then add 1

        if(SubCategoryTexts.Count%2==1)
        {
            SubCategoryBackgroundScaleNumber = SubCategoryBackgroundScaleNumber + 1;
        }
        // Scale the background
        //Vector3 scale = new Vector3(1,SubCategoryBackgroundToScale.GetComponent<RectTransform>().localScale.y,1);
        //scale.y = (SubCategoryBackgroundScaleNumber / 2)* SubScalingNumber;
        //SubCategoryBackgroundToScale.GetComponent<RectTransform>().localScale = scale;
        
        var SubPositionNumber = 29f;

        var SubMenuStartPosition = -98.5f;
        
        // Reset menu position
        var SubMenuPositionReset = new Vector3(0,SubMenuStartPosition,0);
        SubCategoryMain.GetComponent<RectTransform>().localPosition = SubMenuPositionReset;
        
        // Move menu down
        var SubMenuPosition = new Vector3(0,SubCategoryMain.GetComponent<RectTransform>().localPosition.y,0);
        SubMenuPosition.y = SubMenuPosition.y + (SubCategoryBackgroundScaleNumber / 2)* SubPositionNumber;
        SubCategoryMain.GetComponent<RectTransform>().localPosition = SubMenuPosition;
        
        
        
        //Debug.Log("Pressed " + CategoryText + " subcategory button");
        Toggle = 3;
    }



    public void DoneButton()
    {
        // find submenu
        SubCategoryMain = GameObject.FindWithTag("SubCategoryMenu");
        //SubCategoryMenu = SubCategoryMain.transform.Find("Menu").gameObject;
        //SubCategoryFilter = SubCategoryMain.transform.Find("Filter").gameObject;
        //SubCategoryBackground = SubCategoryMain.transform.Find("SubBackground").gameObject;
        
  
        // Check if all subcategories all true
        var Alltrue = filterManager.CategoryFilters.Where(t => t.CategoryName == CategoryText).All(t => t.IsSelected != false);
        if (Alltrue == true)
        {
            Toggle = 1;
            ON();
            //Debug.Log("All subcategories are true");

        }

        // Check if all subcategories all true
        var Allfalse = filterManager.CategoryFilters.Where(t => t.CategoryName == CategoryText).All(t => t.IsSelected != true);
        if (Allfalse == true)
        {
            Toggle = 2;
            OFF();
            //Debug.Log("All subcategories are false");
        }
        
        // Delete subcategories
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("SubCategoryTexts"))
        {
            Destroy(fooObj);
        }
        SubCategoryTexts.Clear();

        LoadColors();

        // Hide submenu
        //SubCategoryMenu.SetActive(false);
        SubCategoryFilter.SetActive(false);
        SubCategoryMenu.SetActive(false);
        SubCategoryDone.SetActive(false);
        SubCategoryHeader.SetActive(false);
    }


    public void ON()
    {
        CategoryMiddle.SetActive(false);
        CategoryOn.SetActive(true);
        CategoryOff.SetActive(false);
        // Find the category name in the list and set it as true
        foreach (var t in filterManager.CategoryFilters.Where(t => t.CategoryName == CategoryText))
        {
            t.IsSelected = true;
        } 
        CategoryButton.interactable = false;
        //Debug.Log("Changed " + CategoryText + " to ON");
    }

 
    public void OFF()
    {
        CategoryOff.SetActive(true);
        CategoryMiddle.SetActive(false);
        CategoryOn.SetActive(false);
         // Find the category name in the list and set it as false
        foreach (var t in filterManager.CategoryFilters.Where(t => t.CategoryName == CategoryText))
        {
            t.IsSelected = false;
        }  
        CategoryButton.interactable = true;
        //Debug.Log("Changed " + CategoryText + " to OFF");
    }

    string UppercaseFirst(string s)
    {
        return string.IsNullOrEmpty(s) ? string.Empty : char.ToUpper(s[0]) + s.Substring(1);
    }
}
