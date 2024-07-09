using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideButton : MonoBehaviour
{

    [SerializeField] private GameObject[] GuideObjects;
    [SerializeField] private GameObject[] GuideTexts;
    [SerializeField] private int CurrentIndex;
    [SerializeField] private bool Clicked;
    [SerializeField] private FadeIn Fade;
    [SerializeField] private GameObject touchIcon;
    [SerializeField] private GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {
        Fade.enabled = true;
        CurrentIndex = 1;
        Clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        // reset index and show buttons and show 1st index
        Clicked = !Clicked;
        if (Clicked)
        {
            ShowGuide();
        }
        else
        {
            HideGuide();
        }
    }
    
    public void ShowGuide()
    {
        // reset index and show buttons and show 1st index
        nextButton.SetActive(true);
        touchIcon.SetActive(true);
        CurrentIndex = 1;
        GuideObjects[CurrentIndex-1].SetActive(true);
        GuideTexts[CurrentIndex-1].SetActive(true);
        
    }
    
    public void HideGuide()
    {
        // hide all objects
        touchIcon.SetActive(false);
        nextButton.SetActive(false);
        foreach (var obj in GuideObjects)
        {
            obj.SetActive(false);
        }
        foreach (var obj in GuideTexts)
        {
            obj.SetActive(false);
        }
        Clicked = false;
        Fade.enabled = true;
    }
    
    public void NextButton()
    {
        // if out of index, turn all off
        if (CurrentIndex >= GuideObjects.Length)
        {
            HideGuide();
        }
        else // hide current index and show next
        {
            GuideObjects[CurrentIndex-1].SetActive(false);
            GuideTexts[CurrentIndex-1].SetActive(false);
            CurrentIndex += 1;
            GuideObjects[CurrentIndex-1].SetActive(true);
            GuideTexts[CurrentIndex-1].SetActive(true);
            Fade.enabled = false;
        }
        
    }
}
