using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteLocations : MonoBehaviour
{
    public List<RouteLocationPointData> routePoints = new List<RouteLocationPointData>();

    private void Start()
    {
        int index = 0;
        foreach (RouteLocationPointData item in routePoints)
        {
            item.pointIndex = index;
            index++;
        }
    }
}
[Serializable]
public class RouteLocationPointData
{
    public int pointIndex;
    public Vector2 position;
}