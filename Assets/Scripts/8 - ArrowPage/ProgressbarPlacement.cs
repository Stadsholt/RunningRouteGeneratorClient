using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressbarPlacement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    [Range(0.0f, 1f)]
    public float value;

    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;
    
    void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        endPoint = GameObject.FindGameObjectWithTag("EndPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, value);
    }
}
