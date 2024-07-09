# <div align="center">RunningRouteGeneratorClient</div>
This project readme was written by fellow group member eske4. The RunningRouteGeneratorClient is a Unity app designed for a University project to create and navigate running routes using the [RunningRouteGeneratorAPI](https://github.com/eske4/RunningRouteGeneratorAPI) and [RunningRouteGeneratorMapAPI](https://github.com/eske4/RunningRouteGeneratorMapAPI). The client has 8 pages, each page with its own features to create the user experience.

## <div align="center">Pages</div>
### Main menu
<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/MainMenu/MainMenu.png" width="150">
</div>

The Main Menu is the starting point where users can either generate a new route or review their route history.

### Route Settings Page
<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/RouteSettingsPage/RouteSettingsFilter.png" width="150" />
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/RouteSettingsPage/RouteSettingsGuide.png" width="150" /> 
</div>
On this page, users can toggle between walk/bike modes, set their home location, and activate explore mode. 

<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/RouteSettingsPage/Explore/RouteSettingsExplore.png" width="150" />
</div>
Explore mode allows users to choose a number of Points of Interest between 0-10, triggering a recommendation algorithm that suggests random points of interest for exploration.


### Points of Interests category page

<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/POICatPage/Categories.png" width="150" />
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/POICatPage/SubCategories.png" width="150" /> 
</div>
If explore mode is not toggled on, users are directed to this page, where they can filter points of interest based on categories available in their area.

### Points of Interests picker page

<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/POIPage/POIPage.jpg" width="150" />
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/POIPage/POIZoom.jpg" width="150" /> 
</div>

After filtering points of interest on the previous page, users are presented with images and names of points of interest in their area under the selected categories. They can choose as many points of interest as they desire.

### Confirm route page

<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/ConfirmPage/Loading.jpg" width="150" />
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/ConfirmPage/RouteConfirm.png" width="150" /> 
</div>

After selecting points of interest, users are shown the generated route on a map, along with distance and estimated time. They can either accept or go back.

### Navigation page

<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/NavigationPage/Navi.jpg" width="150" />
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/NavigationPage/NaviMap.jpg" width="150" /> 
</div>

This page guides users through the route with audio cues, directional arrows, information about upcoming points of interest, a pause button, a progression line, and a menu for route editing and navigation home.

### Afterrun Page

<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/AfterRunPage/AfterRun.jpg" width="150" />
</div>

After completing the route, this page displays statistics such as speed, distance, and points of interest passed. Users can also rate the route, and the rating is sent to a database used by the explore mode for recommendations.

### Route history page

<div>
  <img src="https://github.com/Stadsholt/RunningRouteGeneratorClient/blob/main/Images/HistoryPage/HistoryPic.jpg" width="150" />
</div>

Accessed through the main menu, this page allows users to run a previously generated route and view their ratings for those routes.

## <div align="center">Setup</div>
### Prerequisites

- Android phone
- [Unity 2021.3.19f1](https://unity.com/releases/editor/whats-new/2021.3.19)
- [RunningRouteGeneratorAPI](https://github.com/eske4/RunningRouteGeneratorAPI)
- [RunningRouteGeneratorMapAPI](https://github.com/eske4/RunningRouteGeneratorMapAPI)
- [Google Maps API key](https://developers.google.com/maps)

### Installation Steps

1. **Create the Required API:**
   - Follow the instructions in the [Recreating the API section](https://github.com/eske4/RunningRouteGeneratorAPI) to create the necessary API.

2. **Set Up RunningRouteGeneratorMapAPI:**
   - Follow the instructions [here](https://github.com/eske4/RunningRouteGeneratorMapAPI) to set up RunningRouteGeneratorMapAPI.

3. **Download Unity 2021.3.19f1:**
   - Download Unity version 2021.3.19f1 [here](https://unity.com/releases/editor/whats-new/2021.3.19).

4. **Store repository**
   - If you have Git installed, you can clone the repository using the following command:

    ```bash
    git clone https://github.com/Stadsholt/RunningRouteGeneratorClient.git
    ```

    Alternatively, you can download the project as a ZIP file:

   - Click on the "Code" button at the top of the repository.
   - Select "Download ZIP" from the dropdown menu.
   - After downloading, extract the contents to your desired location on your computer.

4. **Update API Domain in Unity:**
   - Inside the projects `Assets/Scripts/RequestSystem/` directory, open the `Requests.cs` file located at [Assets/Scripts/RequestSystem/Requests.cs].
   - Update the API domain, replacing "insert-your-domain" with your API domain. Modify line 15:
      ```csharp
      public static string BaseApiUrl { get; private set; } = "insert-your-domain";
      ```

5. **Obtain Google Maps API Key:**
   - Obtain a Google Maps API key [here](https://developers.google.com/maps).

6. **Update Google Images Requester:**
   - Inside the projects `Assets/Scripts/RequestSystem/` directory, open the `GoogleImagesRequester.cs` file.
   - Replace the `api_key` variable (line 1) with the obtained Google Maps API key:
      ```csharp
      string api_key = "your-google-maps-api-key";
      ```

7. **Update RunningRouteGeneratorMapAPI URL:**
   - In the `/Assets/Scripts/7 - RouteConfirmPage/UI/` directory, open the 'GetMapImage.cs' file.
   - Replace the `Website string` variable (line 10) with your own RunningRouteGeneratorMapAPI URL created in Step 2:
      ```csharp
      private string Website = "Insert-your-map-API-url/DrawMap/";
      ```

8. **Build and Run on Android:**
   - Upload the app to your Android device by following these steps within Unity:
      - In Unity Hub add the project and open it with `Unity 2021.3.19f1`
      - Go to `File` > `Build Settings`.
      - Select `Android` under the platform.
      - Download the Android module guided by Unity and switch the platform to Android.
      - Build and run the application, making sure to have an Android phone connected to the pc.

**Important:** Never upload this app publicly, as the Google Maps API key is embedded in the app. The API key should have been built into the API during step 1. Consider using an alternative method from Google Maps to avoid potential costs and security.
