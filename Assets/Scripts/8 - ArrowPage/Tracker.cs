using System.Collections.Generic;
using Navigation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Tracker : MonoBehaviour
{
    
    //progression bar controls
    [Header("Progress Bar")]
    [SerializeField] GameObject progressBar;
    [SerializeField] Transform progressBarStartPoint;
    [SerializeField] GameObject POIMarker;
    [SerializeField] ProgressbarPlacement userMarker;

    //Text fields
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI avgPaceText;
    [SerializeField] private TextMeshProUGUI timeSpentText;
    [SerializeField] private TextMeshProUGUI movedDistanceText;
    [SerializeField] private TextMeshProUGUI totalDistance;
    
    private UserArrowRotation _arrowData;
    private StoredData _data;
    private RouteNodeList _routeData;
    private readonly NavigationTools _navTools = new NavigationTools();

    private float _timeSpent;
    private float _totalDistanceMoved;
    private float _distanceMoved;
    private float _routeDistance;
    private float _avgPace;

    private List<double> _poiDistances = new List<double>();
    private int _nodeIndex = 0;
    private UserArrowRotation.NavigationStates _state = UserArrowRotation.NavigationStates.Pause;
    [SerializeField] private TMP_Text DebugText;
    
    void Start()
    {
        _arrowData = GameObject.FindWithTag("UserArrowRotation").GetComponent<UserArrowRotation>();
        _data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();
        _timeSpent = _data.TotalTime;
        _totalDistanceMoved = _data.TotalDistance;
        _routeData = new RouteNodeList(_data.Route);
        _routeDistance = (float)_routeData.RouteDistance;
        totalDistance.SetText(ConvertMetersToKilometers(_routeDistance).ToString("F1") + " km");
        // Get POI route distance
        double tempDistance = 0;

        foreach (var node in _routeData.TurnNodes)
        {
            tempDistance += node.distanceToNode;
            if (node.instructions.Contains("Arrive"))
            {
                _poiDistances.Add(tempDistance);
            }
        }
        
        
        //Remove home poi
        _poiDistances.RemoveAt(_poiDistances.Count - 1);


// Populate progress bar
        for (var index = 0; index < _poiDistances.Count; index++)
        {
            var POI = _poiDistances[index];
            var marker = Instantiate(POIMarker, progressBarStartPoint.transform.position, Quaternion.identity, progressBar.transform);
            marker.GetComponent<ProgressbarPlacement>().value = (float)POI / _routeDistance;
            GameObject POIPicObj = marker.transform.Find("POIPic").gameObject;
            POIPicObj.GetComponent<Image>().sprite = _data.Images[index];
        }
    }

    // Update is called once per frame
    void Update()
    {
        _state = _arrowData.NavigationState;
        if (_nodeIndex < _arrowData.CurrentNodeIndex)
        {
            _totalDistanceMoved += (float)_routeData.TurnNodes[_nodeIndex].distanceToNode;
            _distanceMoved += (float)_routeData.TurnNodes[_nodeIndex].distanceToNode;
            _nodeIndex++;
        }
        switch (_state)
        {
            case UserArrowRotation.NavigationStates.ToNode:
                Time.timeScale = 1; //Continue time
                UpdateStats(_arrowData.CurrentPosition, _arrowData.NextPosition);
                totalDistance.SetText(DistanceLeft());
                userMarker.value = (float)GetCurrentPosition(_arrowData.CurrentPosition, _arrowData.NextPosition) / _routeDistance;
                break;
            case UserArrowRotation.NavigationStates.ToPoi:
                Time.timeScale = 1;
                UpdateStats(_arrowData.CurrentPosition, _arrowData.ImagePosition);
                totalDistance.SetText(DistanceLeft());
                userMarker.value = (float)GetCurrentPosition(_arrowData.CurrentPosition, _arrowData.ImagePosition) / _routeDistance;
                break;
            case UserArrowRotation.NavigationStates.UpdateRoute:
                Time.timeScale = 0; //pause time
                break;
            case UserArrowRotation.NavigationStates.Pause:
                Time.timeScale = 0;
                break;
        }
    }
    

    private void UpdateStats(Vector2 currentPos, Vector2 targetPos)
    {
        _timeSpent += Time.deltaTime;
        _data.TotalTime = _timeSpent;
        _data.TotalDistance = _totalDistanceMoved;
        userMarker.value = (_distanceMoved) / _routeDistance;
        
        float TotalDistanceMovedFloat = float.Parse(LiveMeterCounter(currentPos, targetPos));
        _avgPace = TotalDistanceMovedFloat > 50 ? _timeSpent / (TotalDistanceMovedFloat / 1000) : 0;
        avgPaceText.SetText(PaceSecToMinSec(_avgPace));
        timeSpentText.SetText(SecToMinSec(_timeSpent));
        movedDistanceText.SetText(LiveMeterCounter(currentPos, targetPos));

    }

    private float ConvertMetersToKilometers(float distanceInMeters)
    {
        return distanceInMeters / 1000f;
    }

    private double GetCurrentPosition(Vector2 currentPos, Vector2 targetPos)
    {
        float distance = _navTools.SpatialTool.GetDistance(currentPos, targetPos);
        double distanceWalkedToNextNode = _routeData.TurnNodes[_arrowData.CurrentNodeIndex].distanceToNode - distance;
        return _distanceMoved + distanceWalkedToNextNode;
    }

    private string DistanceLeft()
    {
        var dist = _routeDistance - _distanceMoved;
        return ConvertMetersToKilometers(Mathf.Max(dist, 0)).ToString("F1") + " km";
    }

    private string LiveMeterCounter(Vector2 currentPos, Vector2 targetPos)
    {
        float distance = _navTools.SpatialTool.GetDistance(currentPos, targetPos);
        float distanceWalkedToNextNode = Mathf.Max((float)_routeData.TurnNodes[_arrowData.CurrentNodeIndex].distanceToNode - distance, 0);
        return (Mathf.RoundToInt(distanceWalkedToNextNode + _totalDistanceMoved)).ToString();
    }

    string PaceSecToMinSec(float input)
    {
        int minutes = Mathf.FloorToInt(input / 60f);
        int seconds = Mathf.FloorToInt(input % 60f);
        
        if (_totalDistanceMoved < 50)
        {
            return "--:--";
        }
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    string SecToMinSec(float input)
    {
        float min = Mathf.Floor(input / 60);
        float sec = input % 60;
        return min.ToString("0#") + ":" + sec.ToString("0#");
    }

}
