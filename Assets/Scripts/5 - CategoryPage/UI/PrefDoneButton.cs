using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefDoneButton : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private GameObject SubCategoryMenu;
    [SerializeField] private GameObject SubCategoryBackground;
    [SerializeField] private GameObject SubCategoryFilter;

    [SerializeField] private List<GameObject> DeleteObj = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void Button()
    {
        // Clear submenu Items
          foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("SubCategoryTexts"))
        {
            Destroy(fooObj);
        }

        // disable submenu
        SubCategoryMenu.SetActive(false);
        SubCategoryFilter.SetActive(false);
        SubCategoryBackground.SetActive(false);

    }
}
