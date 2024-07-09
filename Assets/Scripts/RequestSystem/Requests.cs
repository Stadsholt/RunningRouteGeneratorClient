using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;



namespace RequestsUtil{

    public static class RequestConfig {
        public static string BaseApiUrl { get; private set; } = "insert-your-domain";
    }

    public class Requests{

        public static async Task<FilterIdData> GetFilterId(double lat, double longi) {
            var json = await GetData(RequestConfig.BaseApiUrl + "/GetIdList/" + lat + "/" + longi + "/");
            try {
                return ConvertData<FilterIdData>(json);
            }
            catch (System.Exception) { 
                throw;
            }
        }

        public static async Task<POIInfo> GetPois(double lat, double longi, string[] filterids) {
            
            string filterdata = "";
            foreach (var item in filterids)
            {
                filterdata += item + ",";
            }
            
            //Debug.Log(filterdata);
            var json = await GetData(RequestConfig.BaseApiUrl + "/GetPois/" + lat + "/" + longi + "/" + filterdata);
            
            try {
                return ConvertData<POIInfo>(json);
            }
            catch (System.Exception) { 
                throw;
            }
        }
        
        
        public static async Task<RouteData> GetRoute(double lat, double longi, string DrivingProfile, POIInfo PoiInfo) {

            string InputJson = JsonConvert.SerializeObject(PoiInfo.data);
            var json = await GetData(RequestConfig.BaseApiUrl + "/GetRoute/" + lat + "/" + longi + "/"  + DrivingProfile + "/" + InputJson);

            try {
                return ConvertData<RouteData>(json);
            }
            catch (System.Exception) { 
                throw;
            }
        }
        
        
        async static Task<string> GetData(string Url)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(Url);

            webRequest.SendWebRequest();

            while(!webRequest.isDone) {
                await Task.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.ConnectionError) {
                return webRequest.error;
            } 
            else {
                return webRequest.downloadHandler.text;
            }
        }

        static T ConvertData<T>(string json) {
            string jsonString = "{\"data\":" + json + " }";
            jsonString = System.Text.RegularExpressions.Regex.Unescape(jsonString);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public async static Task<string> PostToDatabase(string username, string[] catids, int rating)
        {
            string catidsString = string.Join(",", catids).TrimEnd(',');

            string url = RequestConfig.BaseApiUrl + "/SendRating/" + username + "/" + catidsString + "/" + rating;

            UnityWebRequest webRequest = UnityWebRequest.Get(url);

            webRequest.SendWebRequest();

            while(!webRequest.isDone) {
                await Task.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                return "Rating sent successfully";
            }
            else
            {
                return webRequest.error;
            }
        }

        public static async Task<RandomIdData> GetRandomIdList(string username, double lat, double longi) {
            var json = await GetData(RequestConfig.BaseApiUrl + "/GetRandomIdList/" + username + "/" + lat + "/" + longi);
            try {
                return ConvertData<RandomIdData>(json);
            }
            catch (System.Exception) { 
                throw;
            }
        }
    }
}