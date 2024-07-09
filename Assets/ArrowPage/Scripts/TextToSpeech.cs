using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToSpeech : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip turnLeftClip;
    [SerializeField] private AudioClip turnRightClip;
    [SerializeField] private AudioClip turnSharpLeftClip;
    [SerializeField] private AudioClip turnSharpRightClip;
    [SerializeField] private AudioClip turnSlightLeftClip;
    [SerializeField] private AudioClip turnSlightRightClip;
    [SerializeField] private AudioClip keepLeftClip;
    [SerializeField] private AudioClip keepRightClip;
    [SerializeField] private AudioClip arrivePoiClip;
    [SerializeField] private AudioClip routeFinishedClip;
    [SerializeField] private AudioClip straightClip;
    [SerializeField] private AudioClip defaultClip;


    private AudioSource audSrc;

    private void Awake()
    {
        audSrc = GetComponent<AudioSource>();
    }

    [ContextMenu("Play")]
    public void PlayClip()
    {
        audSrc.Play();
    }

    public void ChangeAudioClip(string inputString)
    {
        if(inputString.Contains("onto"))
        {
            string[] newInput = inputString.Split('o');
            inputString = newInput[0].TrimEnd();
        }
        switch (inputString)
        {
            case "Turn left":
                audSrc.clip = turnLeftClip;
                break;
            case "Turn right":
                audSrc.clip = turnRightClip;
                break;
            case "Turn sharp left":
                audSrc.clip = turnSharpLeftClip;
                break;
            case "Turn sharp right":
                audSrc.clip = turnSharpRightClip;
                break;
            case "Turn slight left":
                audSrc.clip = turnSlightLeftClip;
                break;
            case "Turn slight right":
                audSrc.clip = turnSlightRightClip;
                break;
            case "Keep left":
                audSrc.clip = keepLeftClip;
                break;
            case "Keep right":
                audSrc.clip = keepRightClip;
                break;
            case "Continue straight":
                audSrc.clip = straightClip;
                break;
            default:
                audSrc.clip = defaultClip;
                break;
        }
        if(inputString.Contains("Arrive"))
        {
            audSrc.clip = arrivePoiClip;
        }
        if(inputString.Contains("Arrive at your destination"))
        {
            audSrc.clip = routeFinishedClip;
        }
    }
}
