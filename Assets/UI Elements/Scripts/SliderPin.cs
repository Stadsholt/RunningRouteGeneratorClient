using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderPin : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int sliderValue;
    [SerializeField] private TextMeshProUGUI pinText;
    [SerializeField] private GameObject pin;

    [SerializeField] private bool isDragging;

    [SerializeField] private bool hideWhenInactive;

    void Start()
    {   
        slider = GetComponent<Slider>();
        isDragging = !hideWhenInactive;
        pin.SetActive(!hideWhenInactive);
    }

    private void Update()
    {
        
        if (isDragging)
        {
            sliderValue = (int)slider.value;
            pinText.text = sliderValue.ToString();
        }
    }

    public void ShowSlider()
    {
        pin.SetActive(true);
        isDragging = true;
    }

    public void HideSlider()
    {
        isDragging = false;
        pin.SetActive(!hideWhenInactive);
    }
}
