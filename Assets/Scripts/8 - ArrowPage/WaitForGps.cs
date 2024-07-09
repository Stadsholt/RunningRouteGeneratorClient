using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForGps : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] ObjectsToActivate;
    
    [SerializeField] private GameObject ObjectToDeactivate;
    [SerializeField] private bool TestToggle;
    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running || TestToggle)
        {
            foreach (var obj in ObjectsToActivate)
            {
                obj.SetActive(true);
            }
            ObjectToDeactivate.SetActive(false);

            Destroy(this);
        }
        Debug.Log("Location not found");
    }
}
