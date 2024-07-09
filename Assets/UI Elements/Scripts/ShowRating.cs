using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;

public class ShowRating : MonoBehaviour
{
    [SerializeField] Sprite starOutline;
    [SerializeField] Sprite starFilled;
    [SerializeField] [Range(1,5)] public int rating;
    [SerializeField] private SVGImage[] stars;



    void Start()
    {
        
    }

    void Update() 
    {
        foreach (var item in stars)
        {
            item.sprite = starOutline;
        }
        for (int i = 0; i < rating; i++)
        {
            stars[i].sprite = starFilled;
        }
    }

}
