using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseZoom : MonoBehaviour
{
    [SerializeField] private GameObject Image;
    
    [SerializeField] private GameObject ZoomPic;
    [SerializeField] private GameObject ExitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseTheZoom()
    {
        Image.SetActive(false);
        ZoomPic.SetActive(false);
        ExitButton.SetActive(false);
    }
}
