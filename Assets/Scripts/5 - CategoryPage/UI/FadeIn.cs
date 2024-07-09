using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image img;
    public bool FadeInToggle = true;
    public float FadeInStartOpacity = 0;
    public float FadeInOpacity = 1;
    public float FadeSpeed = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        img.color = new Color(1, 1, 1, FadeInStartOpacity);

        if (FadeInToggle == true)
        { 
            StartCoroutine(FadeImage(false));
        }
        if (FadeInToggle == false)
        {
            StartCoroutine(FadeImage(true));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * FadeSpeed)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= FadeInOpacity; i += Time.deltaTime * FadeSpeed)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}


