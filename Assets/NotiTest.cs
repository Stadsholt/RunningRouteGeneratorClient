using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotiTest : MonoBehaviour
{

    public GameObject NotiPrefab;

    public GameObject PrefabParent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        
        var notification = new AndroidNotification();
        notification.Title = "Point of interest reached!";
        notification.Text = "You have reached X";
        notification.FireTime = System.DateTime.Now.AddMinutes(0);
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
}
