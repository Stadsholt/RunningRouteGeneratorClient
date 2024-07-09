using System;
using UnityEngine;
using UnityEngine.UI;
using Navigation;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Serialization;
using RequestsUtil;
using Unity.VectorGraphics;
using UnityEditor;
using UnityEngine.SceneManagement;


public class UserArrowRotation : MonoBehaviour
{
    // Adjustable Parameters in Unity Inspector
    [Header("State settings")]
    [SerializeField] private int pointOfInterestProximityThreshold = 100;
    [SerializeField] private int nodeProximityThreshold = 15;
  
    [FormerlySerializedAs("nodeInstructionProximityThreshold")]
    [Header("Instruction settings")]
    [SerializeField] private int nodeAheadInstructionProximityThreshold = 40;
    [SerializeField] private int lastNodeInstructionProximityThreshold = 15;
    [SerializeField] private int audioInstructionProximityThreshold = 40;

    [Header("Update route settings")]
    [SerializeField] private int pointOfInterestDirectionThreshold = 50;
    [SerializeField] private int updateRouteDirectionThreshold = 100;

    // Navigation tools
    private readonly NavigationTools _navTools = new NavigationTools();
    [SerializeField] private VoiceSystemNavigation _voiceSystem;
    [SerializeField] private Toggle MuteToggle;
    
    // Data and lists
    private StoredData _data;
    private RouteNodeList _routeNodeList;
    private string _currentInstruction;
    private NavigationStates _lastState;

    // Current indexes and positions
    public int CurrentNodeIndex { get; private set; }
    private int _currentImageIndex = 0;
    public Vector2 NextPosition { get; private set; }
    public Vector2 ImagePosition { get; private set; }
    public Vector2 CurrentPosition { get; private set; }
    private Vector2 _lastPosition;
    private Sprite _currentImage;
    private double _currentNodeDist;
    private string _currentAudioInstructions;

    // Arrays to access
    private List<string[]> _pointOfInterestsList;
    private Vector2[] _nodeLocations;
    private string[] _nodeInstructions;
    private Sprite[] _pointOfInterestPictures;
    private double[] _nodeDistances;
    
    //UI elements
    [Header("UI elements")]
    public SVGImage image;
    [SerializeField] private GameObject arrow;
    [SerializeField] private TMP_Text nextCoordinateDistanceDisplay;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private Button[] pauseButtons;


    //[SerializeField] private Button MuteToggleButton;

    // Flag to check if audio is played
    bool audioPlayed = false;
    
    //Home picture that is set when going home
    [Header("Input elements")]
    [SerializeField] private Sprite homeSprite;
    [SerializeField] private TMP_Text imageTitle;
    
    //Different states it toggles between
    public enum NavigationStates
    {
        ToNode,
        ToPoi,
        UpdateRoute,
        Pause,
    }
    
    //Default state set before app is running
    public NavigationStates NavigationState { get; private set; }
    
    private void Start()
    {
        InitializeNavigationSystem();
        InitializeData();
    }
    
    private void Update()
    {
        // Update the current position based on the device's location
        CurrentPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);

        // Update the next position based on the current node index
        NextPosition = _nodeLocations[CurrentNodeIndex];
        
        // Update the current audio instructions if there are more instructions in the list
        if (CurrentNodeIndex < _nodeInstructions.Length - 1)
            _currentAudioInstructions = _nodeInstructions[CurrentNodeIndex + 1];
        
        // Update the current instruction, image, and node distance
        _currentInstruction = _nodeInstructions[CurrentNodeIndex];
        _currentImage = _pointOfInterestPictures[_currentImageIndex];
        _currentNodeDist = _nodeDistances[CurrentNodeIndex];


        // Update the image sprite with the current image to ui element
        image.sprite = _currentImage;

        // Handle different navigation states
        switch (NavigationState)
        {
            case NavigationStates.ToNode:
                // Navigate to the next node
                NavigateToNode();
                break;
            case NavigationStates.ToPoi:
                // Navigate to the point of interest
                NavigateToPointOfInterest();
                break;
            case NavigationStates.UpdateRoute:
                // Display "Updating route" while updating the route
                _navTools.TextHandler.SwitchText(instructionText, "Updating route");
                break;
            case NavigationStates.Pause:
                // Do nothing when navigation is paused
                break;
        }
    }
    
    private void InitializeNavigationSystem()
    {
        //_voiceSystem = gameObject.AddComponent<VoiceSystemNavigation>();
        NavigationState = NavigationStates.ToNode;

        // Register pause button listener
        foreach (var button in pauseButtons)
        {
            button.onClick.AddListener(TaskOnClick);
        }
        
        //MuteToggleButton.onClick.AddListener(ToggleMute);

        // Prevent app from being closed by the lock screen
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    private void InitializeData()
    {
        // Get the StoredData component from the GameObject with the "StoredData" tag
        _data = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();

        // Initialize the RouteNodeList with the stored route data
        _routeNodeList = new RouteNodeList(_data.Route);
  
        // Set the initial current node index to 0
        CurrentNodeIndex = 0;

        // Get the list of selected points of interest
        _pointOfInterestsList = _data.SelectedPointsOfInterests.data;
        
        // Extract the node locations, instructions, and distances from the RouteNodeList
        _nodeLocations = _routeNodeList.TurnNodes.Select(node => node.coordinates).ToArray();
        _nodeInstructions = _routeNodeList.TurnNodes.Select(node => node.instructions).ToArray();
        _nodeDistances = _routeNodeList.TurnNodes.Select(node => node.distanceToNode).ToArray();

        // Combine the point of interest images with the home sprite
        _pointOfInterestPictures = _data.Images.Concat(new[] { homeSprite }).ToArray();

        // Get the last known device location
        _lastPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);

        // Calculate the image position from the point of interest data
        ImagePosition = new Vector2(float.Parse(_pointOfInterestsList[_currentImageIndex][3]),
            float.Parse(_pointOfInterestsList[_currentImageIndex][2]));

        // Set title
        imageTitle.SetText(_pointOfInterestsList[_currentImageIndex][0]);
    }

    private void NavigateToNode()
{
    // Update instruction text and display distance to the next node
    _navTools.TextHandler.SwitchText(instructionText, "Continue straight");
    _navTools.TextHandler.DisplayMeters(nextCoordinateDistanceDisplay,
        _navTools.SpatialTool.GetDistance(CurrentPosition, NextPosition));

    // Rotate the arrow towards the next node
    _navTools.SpatialTool.ArrowRotateTowardPoint(NextPosition, arrow);

    // Update the route if the user is going in the wrong direction
    if (_navTools.SpatialTool.IsGoingInWrongDirection(CurrentPosition, NextPosition, _currentNodeDist, updateRouteDirectionThreshold))
    {
        UpdateRoute();
    }

    // Check if the user is within 15 meters of the next node
    if (_navTools.SpatialTool.IsDistanceWithinMeters(CurrentPosition, NextPosition, nodeProximityThreshold))
    {
        Handheld.Vibrate();
        
        // If the user has reached the last node, proceed to the AfterRunPage
        if (CurrentNodeIndex == _nodeLocations.Length - 1)
            {
                _voiceSystem.PlayAudioOnInstrucs(_currentAudioInstructions);
                _data.SelectedPointsOfInterests.data = _data.POIPassed;
                _data.Images = _data.CurrentSprites.ToArray();
                SceneManager.LoadScene("9 - AfterRunPage");
            }

        _lastPosition = CurrentPosition;
        CurrentNodeIndex++;
        audioPlayed = false;
    }

    // Update the instruction text if the user is close to the last position or the next position
    if (_navTools.SpatialTool.IsDistanceWithinMeters(CurrentPosition, _lastPosition, lastNodeInstructionProximityThreshold) ||
        _navTools.SpatialTool.IsDistanceWithinMeters(CurrentPosition, NextPosition, nodeAheadInstructionProximityThreshold))
    {
        _navTools.TextHandler.SwitchText(instructionText, _currentInstruction);
    }

    // Play audio instructions if the user is within 40 meters of the next position and audio hasn't been played yet
    if (_navTools.SpatialTool.IsDistanceWithinMeters(CurrentPosition, NextPosition, audioInstructionProximityThreshold) && !audioPlayed)
    {
        if (MuteToggle.isOn == false)
        {
            _voiceSystem.PlayAudioOnInstrucs(_currentAudioInstructions);
        }
        audioPlayed = true;
    }

    // Switch to the ToPoi state if the user is within 100 meters of a point of interest and not at the last element
    if (_navTools.SpatialTool.IsDistanceWithinMeters(CurrentPosition, ImagePosition, pointOfInterestProximityThreshold))
    {
        if (_currentImageIndex != _pointOfInterestPictures.Length - 1)
        {
            NavigationState = NavigationStates.ToPoi;
        }
    }
}

 
    private void NavigateToPointOfInterest()
    {
        // Display the approaching point of interest text
        _navTools.TextHandler.SwitchText(instructionText, "Arriving at: " + _pointOfInterestsList[_currentImageIndex][0]);
        
        // Update the distance display with the distance between the current position and the image position
        _navTools.TextHandler.DisplayMeters(nextCoordinateDistanceDisplay,
            _navTools.SpatialTool.GetDistance(CurrentPosition, ImagePosition));
        
        // Rotate the arrow towards the point of interest
        _navTools.SpatialTool.ArrowRotateTowardPoint(ImagePosition, arrow);

        // Update the route if going in the wrong direction
        if (_navTools.SpatialTool.IsGoingInWrongDirection(CurrentPosition, ImagePosition, pointOfInterestDirectionThreshold, updateRouteDirectionThreshold))
        {
            UpdateRoute();
        }
  
        // Check if the user is within 15 meters of the point of interest
        if (_navTools.SpatialTool.IsDistanceWithinMeters(CurrentPosition, ImagePosition, nodeProximityThreshold))
        {
            // Send a custom notification for the point of interest
            _navTools.NotificationHandler.CustomNofitication(_pointOfInterestsList[_currentImageIndex][0]);
  
            // Vibrate the device
            Handheld.Vibrate();

            // Save the data if the user edits the route and avoid duplicates of images
            if (!_data.POIPassed.Any(s => s.SequenceEqual(_pointOfInterestsList[_currentImageIndex])))
            {    
                _data.CurrentSprites.Add(_data.Images[_currentImageIndex]);
            }
         
            // Add the point of interest to the passed list
            _data.POIPassed.Add(_pointOfInterestsList[_currentImageIndex]);
      
            // Increment the image index
            _currentImageIndex++;

            // Update the image position
            ImagePosition = new Vector2(float.Parse(_pointOfInterestsList[_currentImageIndex][3]),
                float.Parse(_pointOfInterestsList[_currentImageIndex][2]));

            // when going home, set text to going home
            if (_currentImageIndex == _pointOfInterestPictures.Length - 1)
            {
                imageTitle.SetText("Going home");
            }
            else
            {
                imageTitle.SetText(_pointOfInterestsList[_currentImageIndex][0]);
            }
            
            // Switch back to the NavigateToNode state
            NavigationState = NavigationStates.ToNode;
        }
       
    }


    private async void UpdateRoute() // Update route when user goes in wrong direction
    {
        // Change the navigation state to update the route
        NavigationState = NavigationStates.UpdateRoute;

        // Update the images array based on the current image index
        _data.Images = new ArraySegment<Sprite>(_pointOfInterestPictures, _currentImageIndex,
            _pointOfInterestPictures.Length - _currentImageIndex - 1).ToArray();

        // Remove the visited points of interest from the data
        _data.SelectedPointsOfInterests.data.RemoveRange(0, _currentImageIndex);

        // Fetch the new route using the current location and selected points of interest
        _data.Route = await Requests.GetRoute(Input.location.lastData.latitude, Input.location.lastData.longitude,
            _data.DrivingProfile, _data.SelectedPointsOfInterests);

        // Load the arrow navigation scene
        SceneManager.LoadScene("8 - ArrowPage");
    }


    private void TaskOnClick()
    {
        // This function handles the pause button interaction
        // It toggles between the current navigation state and the paused state
    
        if (NavigationState == NavigationStates.Pause)
        {
            // If the current state is 'Pause', restore the previous state
            NavigationState = _lastState;
        }
        else
        {
            // If the current state is not 'Pause', store the current state
            // and set the navigation state to 'Pause'
            _lastState = NavigationState;
            NavigationState = NavigationStates.Pause;
        }
    }
}