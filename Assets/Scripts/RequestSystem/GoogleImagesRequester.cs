using System;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using RequestsUtil;

namespace RequestsImage{
  

public class GooglePlacesImageURL
    {
    private ImageRequester imageRequester;


    string api_key = "";


    public async Task<Sprite> NearbySearch(string placeName, string longitude, string latitude)
    {
        string location = longitude + ", " + latitude;
        string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";

        string parameters = "&key=" + api_key +
                    "&keyword=" + placeName +
                    "&location=" + location +
                    "&radius=500";

        string idCall = url + parameters;     

        string placedata = await ImageRequester.GetText(idCall);

        PlaceDetails placeDetails = JsonConvert.DeserializeObject<PlaceDetails>(placedata);

        string photoRef = placeDetails.results[0].photos[0].photo_reference;

            string photoUrl = "https://maps.googleapis.com/maps/api/place/photo?";
            string photoParameters = "&key=" + api_key + "&photo_reference=" + photoRef + "&maxwidth=500";

            string finalPhotoUrl = photoUrl + photoParameters;
            return await ImageRequester.GetImageFromUrl(finalPhotoUrl);
        }
    }

    [System.Serializable]
    public class PlaceDetails
    {
        public Result[] results { get; set; }
    }
    [System.Serializable]
    public class Result
    {
        public string name { get; set; }
        public Photo[] photos { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
        public string vicinity { get; set; }
    }

    [System.Serializable]
    public class Geometry
    {
        public Location location { get; set; }
    }

    [System.Serializable]
    public class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Photo
    {
        public int height { get; set; }
        public string[] html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }
}
