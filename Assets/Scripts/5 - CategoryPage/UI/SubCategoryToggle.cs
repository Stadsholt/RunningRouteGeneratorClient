using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FilteringSystem;

public class SubCategoryToggle : MonoBehaviour
{
    [Header("Filter system")]
    [SerializeField] private CategoryFilterManager filterManager;
     [SerializeField] private FilterSystem FilterSystem;

    [Header("Data")]
    [SerializeField] private Transform SubCategoryTextBox;
    [SerializeField] private string SubCategoryText;
    [SerializeField] private string SubCategoryTextDisplay;
    [SerializeField] private string SubCategoryID;
    [SerializeField] private bool Toggle = false;

     [SerializeField] private Button SubCategoryButton;

    [SerializeField] private Color OnColor;
    [SerializeField] private Color OnHighColor;
    [SerializeField] private Color OnPressedColor;
    
    [SerializeField] private Color OffColor;
    [SerializeField] private Color OffHighColor;
    [SerializeField] private Color OffPressedColor;

    // Start is called before the first frame update
    void Start()
    {
        FilterSystem = FindObjectOfType<FilterSystem>();
        filterManager = FilterSystem.filterManager;


        Toggle = false;
        SubCategoryButton = SubCategoryTextBox.GetComponentInChildren<Button>();
        SubCategoryID = SubCategoryTextBox.GetComponentInChildren<Text>().text;
        SubCategoryText = SubCategoryTextBox.GetComponentInChildren<TextMeshProUGUI>().text;
        
        switch (SubCategoryText)
        {
            case "bureau_de_change":
                SubCategoryTextDisplay = "Currency exchange";
                break;
            case "chalet":
                SubCategoryTextDisplay = "Wooden cabin";
                break;
            case "animal_boarding":
                SubCategoryTextDisplay = "Animal hotel";
                break;
            case "stop_position":
                SubCategoryTextDisplay = "Bus stop";
                break;
            default:
                SubCategoryTextDisplay = UppercaseFirst(SubCategoryText);
                SubCategoryTextDisplay = SubCategoryTextDisplay.Replace("_", " ");
                break;
        }
        SubCategoryTextBox.GetComponentInChildren<TextMeshProUGUI>().text = SubCategoryTextDisplay;
       

    // load existing toggle values
    foreach (var t in filterManager.CategoryFilters.Where(t => t.SubcategoryName == SubCategoryText))
    {
        switch (t.IsSelected)
        {
            case true:
                //SubCategoryTextBox.GetComponentInChildren<Toggle>().isOn = true;
                ON();
                break;
            case false:
                //SubCategoryTextBox.GetComponentInChildren<Toggle>().isOn = false;
                OFF();
                break;
        }
    } 
    }

    // Update is called once per frame
    void Update()
    {
    }

  public void Click()
    {
        Toggle = !Toggle;

        if (Toggle)
        {
            ON();
        }
        else
        {
            OFF();
        }
    }


 public void ON()
    {
        // Find the SubCategory name in the list and set it as true
       
        foreach (var t in filterManager.CategoryFilters.Where(t => t.SubcategoryName == SubCategoryText))
        {
            t.IsSelected = true;
        }
        var cb1 = SubCategoryButton.GetComponentInChildren<Button>().colors;
        cb1.normalColor = OnColor;
        cb1.selectedColor = OnColor;
        cb1.highlightedColor = OnHighColor;
        cb1.pressedColor = OnPressedColor;
        SubCategoryButton.GetComponentInChildren<Button>().colors = cb1;
    }

 
    public void OFF()
    {
         // Find the SubCategory name in the list and set it as false
        foreach (var t in filterManager.CategoryFilters.Where(t => t.SubcategoryName == SubCategoryText))
        {
            t.IsSelected = false;
        }
        var cb2 = SubCategoryButton.GetComponentInChildren<Button>().colors;
        cb2.normalColor = OffColor;
        cb2.selectedColor = OffColor;
        cb2.highlightedColor = OffHighColor;
        cb2.pressedColor = OffPressedColor;
        SubCategoryButton.GetComponentInChildren<Button>().colors = cb2;
    }


      string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
