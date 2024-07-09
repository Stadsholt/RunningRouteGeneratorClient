using UnityEngine;
using UnityEngine.UI;

public class FetchMap : MonoBehaviour
{
    public StoredData data;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        var spriteRenderer = gameObject.GetComponent<Image>();
        spriteRenderer.sprite = data.MapImg;
    }

}