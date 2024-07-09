using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FilteringSystem;
using RequestsUtil;

public class FilterSystem : MonoBehaviour
{
    public StoredData data;
    [SerializeField]private GameObject[] objectToActivate;

    [SerializeField]private GameObject[] objectsToDeactivate;
    
    FilterIdData categoryData;
    public CategoryFilterManager filterManager;

    public string[] SelectedData{get; private set;}
    
    [SerializeField] private Button RouteGenButton;

    private static GameObject sampleInstance;

    async void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        
        RouteGenButton.onClick.AddListener(OnClick);

        //filterManager.CategoryFilters

        categoryData = await Requests.GetFilterId(data.StartCoords.x, data.StartCoords.y);
        filterManager = new CategoryFilterManager(categoryData);



        foreach (var obj in objectToActivate)
        {
            obj.SetActive(true);
        }
        
        foreach (var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }

    public void OnClick()
    {
        SelectedData = filterManager.GetFilters(filterManager.CategoryFilters);
        data.SelectedFilterData = SelectedData;
    }
}