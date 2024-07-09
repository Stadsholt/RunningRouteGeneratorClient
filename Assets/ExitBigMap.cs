using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBigMap : MonoBehaviour
{
    [SerializeField] private GameObject BigMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseBigMap()
    {
        BigMap.SetActive(false);
    }
}
