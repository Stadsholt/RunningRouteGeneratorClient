using UnityEngine;
using RequestsUtil;
using UnityEngine.SceneManagement;

public class RefreshRoute : MonoBehaviour
{
    [SerializeField]private GameObject objectToActivate;
    private StoredData datas;
    // Start is called before the first frame update
    async void Start()
    {
        datas = GameObject.FindWithTag("StoredData").GetComponent<StoredData>();

        // if route has no POIs
        if (datas.Images.Length == 0)
        {
            SceneManager.LoadScene("1 - MainPage");
        }
        datas.SelectedPointsOfInterests.data.Add(new[] {"Home", "HomeID", datas.HomeCoords.y.ToString(), datas.HomeCoords.x.ToString()});
        var Route = await Requests.GetRoute(datas.StartCoords.x, datas.StartCoords.y, datas.DrivingProfile, datas.SelectedPointsOfInterests);
        
        Route.data.features[0].properties.summary = datas.Route.data.features[0].properties.summary;
        datas.Route = Route;
        
        objectToActivate.SetActive(true);
    }
}