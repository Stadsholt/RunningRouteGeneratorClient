using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDublicates : MonoBehaviour
{

    [SerializeField] private List<GameObject> DeleteObj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        // Unity bugs out and leaves clones after run time is over sometimes?

        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("CategoryTexts"))
        {
            Destroy(fooObj);
            Debug.Log("Dublicate CategoryText found and deleted");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
