using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStoredData : MonoBehaviour
{
    void Awake()
    {
        GameObject storedData = GameObject.FindGameObjectWithTag("StoredData");
        if (storedData != null)
        {
            Debug.Log("Destroying LocationSystem object");
            Destroy(storedData);
        }
    }
}