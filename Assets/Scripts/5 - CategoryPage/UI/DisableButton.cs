using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    [SerializeField] private GameObject SubCategoryMenu;

    [SerializeField] private Button MyButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SubCategoryMenu.activeSelf)
        {
            MyButton.interactable=false;
        }
        else
        {
            MyButton.interactable=true;
        }


    }
}
