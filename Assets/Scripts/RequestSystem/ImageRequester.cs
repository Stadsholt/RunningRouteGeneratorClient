using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace RequestsImage{
    public class ImageRequester
    {
    public static async Task<Sprite> GetImageFromUrl(string url) {
            
            try
            {
                Sprite newSprite = await GetImage(url);
                Debug.Log("Success");
                return newSprite;
            }
            catch (System.Exception) { 
                throw;
            }
        }

    public async static Task<Sprite> GetImage(string Url)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(Url);

            webRequest.SendWebRequest();

            while(!webRequest.isDone) {
                await Task.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.ConnectionError) {
                throw new Exception(webRequest.error);
            } 
            else {
                DownloadHandlerTexture downloadHandlerTexture = webRequest.downloadHandler as DownloadHandlerTexture;
                return Sprite.Create(downloadHandlerTexture.texture, new Rect(0, 0, downloadHandlerTexture.texture.width, downloadHandlerTexture.texture.height), new Vector2(.5f, .5f));
            }
        }

    public async static Task<string> GetText(string Url)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(Url);

            webRequest.SendWebRequest();

            while(!webRequest.isDone) {
                await Task.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.ConnectionError) {
                Debug.Log("Either no more query requests for this key or no image");
                return webRequest.error;
            } 
            else {
                return webRequest.downloadHandler.text;
            }
        }
    }
}
