using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RequestsUtil;
using RequestsImage;
using Unity.VectorGraphics;

public class getimg : MonoBehaviour
{
public GameObject Parent;

public FetchPOISystem FetchPoi;

public bool Loaded = false;
public string Name;
public string Lat;
public string Long;
private StoredData datas;

[SerializeField] private SVGImage spriteRenderer;

    // Start is called before the first frame update
    async void Start()
    {
        Loaded = false;

        
        //var spriteRenderer = gameObject.GetComponent<SVGImage>();
        var googlePlacesImageURL = new GooglePlacesImageURL();
        try
        {
            Sprite image = await googlePlacesImageURL.NearbySearch(Name, Long, Lat);
            spriteRenderer.sprite = image;
            spriteRenderer.color = Color.white;
            Loaded = true;
            //Debug.Log("LOADED");
        }
        catch (System.Exception)
        {
            Loaded = false;
            FetchPoi.AmountOfPOI--;
            Destroy(Parent);
        }

        
        
    }

    // Update is called once per frame
}
