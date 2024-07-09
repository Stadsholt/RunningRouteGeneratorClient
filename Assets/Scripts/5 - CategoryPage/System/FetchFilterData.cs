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


public class FetchFilterData : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<GameObject> CategoryTexts;

    [Header("Objects")]
    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject PrefMenu;
    [SerializeField] private GameObject CategoryBackground;
    

    
    public FilterSystem filterSystem;


    // Start is called before the first frame update
        void Start()
    {
        filterSystem = GameObject.Find("FilteringSystem").GetComponent<FilterSystem>();
        
        //filterManager.CategoryFilters[0].IsSelected = true;


        // Create category text boxes
        for (int i = 0; i < filterSystem.filterManager.Categories.Count; i++)
        {
            GameObject obj = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity, PrefMenu.transform) as GameObject;
            //obj.transform.parent = PrefMenu.transform;

            CategoryTexts.Add(obj);
        }


        // Set all the text and make boxes visible
        for (int i = 0; i < filterSystem.filterManager.Categories.Count; i++)
        { 
            CategoryTexts[i].GetComponentInChildren<TextMeshProUGUI>().text = filterSystem.filterManager.Categories.ToList()[i];
            CategoryTexts[i].transform.parent.gameObject.SetActive(true);
        }


        //CategoryBackgroundScaleNumber = CategoryTexts.Count;
        //// If amount of category boxes is odd, then add 1 to scale

        //ScalingNumber = 1.25f;
        //if(CategoryTexts.Count%2==1)
        //{
        //    CategoryBackgroundScaleNumber = CategoryBackgroundScaleNumber + 1;
        //}
        //// Scale the background
        //Vector3 scale = new Vector3(1,CategoryBackground.GetComponent<RectTransform>().localScale.y,1);
        //scale.y = (CategoryBackgroundScaleNumber / 2)* ScalingNumber;
        //CategoryBackground.GetComponent<RectTransform>().localScale = scale;



        //CategoryBackground.GetComponent<RectTransform>().localScale.y = CategoryBackgroundScaleNumber * ScalingNumber;



    }

   
    

    // Update is called once per frame
    void Update()
    {

        
    }
}