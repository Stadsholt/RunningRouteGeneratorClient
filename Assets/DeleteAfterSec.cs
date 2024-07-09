using UnityEngine;
using System.Collections;
 
public class DeleteAfterSec: MonoBehaviour
{
    public float lifetime;
 
    void Start ()
    {
        Destroy (gameObject, lifetime);
    }
}