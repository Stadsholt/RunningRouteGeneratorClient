using System.Globalization;
using UnityEngine;
using Unity.Notifications.Android;
using TMPro;

namespace Navigation
{
    public class NavigationTools
    {
        public SpatialTool SpatialTool { get; }
        public TextHandler TextHandler { get; }
        public NotificationHandler NotificationHandler { get; }

        public NavigationTools()
        {
            SpatialTool = new SpatialTool();
            TextHandler = new TextHandler();
            NotificationHandler = new NotificationHandler();
        }
    }
    public class SpatialTool
    {
        private const float EarthRadiusMeters = 6371000;
        private float GetBearing(Vector2 currentLocation, Vector2 targetLocation)
        {
            // Convert the latitude and longitude of the locations to radians
            float currentLat = currentLocation.x * Mathf.Deg2Rad;
            float currentLon = currentLocation.y * Mathf.Deg2Rad;
            float targetLat = targetLocation.x * Mathf.Deg2Rad;
            float targetLon = targetLocation.y * Mathf.Deg2Rad;

            // Calculate the difference in longitude between the locations
            float deltaLon = targetLon - currentLon;

            // Calculate the bearing using the Haversine formula
            float y = Mathf.Sin(deltaLon) * Mathf.Cos(targetLat);
            float x = Mathf.Cos(currentLat) * Mathf.Sin(targetLat) -
                      Mathf.Sin(currentLat) * Mathf.Cos(targetLat) * Mathf.Cos(deltaLon);
            float bearingRad = Mathf.Atan2(y, x);
            float bearingDeg = bearingRad * Mathf.Rad2Deg;

            // Make sure the bearing is between 0 and 360 degrees
            if (bearingDeg < 0)
            {
                bearingDeg += 360;
            }

            return bearingDeg;
        }
        private float GetDistanceBetweenGeoCoordinates(Vector2 startCoord, Vector2 targetLocation)
        {
            float lat1 = Input.location.lastData.latitude;
            float lon1 = Input.location.lastData.longitude;

            float lat2 = targetLocation.x;
            float lon2 = targetLocation.y;

            float dLat = Mathf.Deg2Rad * (lat2 - lat1);
            float dLon = Mathf.Deg2Rad * (lon2 - lon1);

            float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                      Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                      Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);

            float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

            float distance = EarthRadiusMeters * c;
            return distance;
        }

        public bool IsDistanceWithinMeters(Vector2 position, Vector2 targetPosition, int meters)
        {
            {
                return GetDistanceBetweenGeoCoordinates(position, targetPosition) <= meters;
            }
        }

        public bool IsGoingInWrongDirection(Vector2 currentPosition, Vector2 targetPosition, double distanceToNextCoordinate, double threshold)
        {
            return GetDistanceBetweenGeoCoordinates(currentPosition, targetPosition) > distanceToNextCoordinate + threshold;
        }

        public float GetDistance(Vector2 startCoord, Vector2 targetLocation)
        {
            return GetDistanceBetweenGeoCoordinates(startCoord, targetLocation);
        }

        public void ArrowRotateTowardPoint(Vector2 point, GameObject arrow)
        {
            const float TurnTime = 300;
        
            Vector2 locationData = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);

            float dir = GetBearing(locationData, point) - Input.compass.trueHeading;
            Quaternion target = Quaternion.Euler(0, 0, -dir);
            Quaternion rotateAmount = arrow.transform.rotation * Quaternion.Inverse(target);
            float rotAmount = rotateAmount.eulerAngles.z < 180
                ? rotateAmount.eulerAngles.z
                : 360 - rotateAmount.eulerAngles.z;

            arrow.transform.rotation = Quaternion.RotateTowards(arrow.transform.rotation, target,
                TurnTime * Time.deltaTime * (rotAmount / 180));
        }
    }

    public class TextHandler
    {
        public void SwitchText(TMP_Text text, string input)
        {
            text.SetText(input);
        }
        
        public void DisplayMeters(TMP_Text text, float input)
        {
            text.SetText(Mathf.RoundToInt(input).ToString(CultureInfo.CurrentCulture) + "m");
        }
    }

    public class NotificationHandler
    {
        public void CustomNofitication(string name)
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
            notification.Text = "You have reached " + name;
            notification.FireTime = System.DateTime.Now.AddMinutes(0);
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }
    }
}
