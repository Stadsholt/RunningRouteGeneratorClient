using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public string SceneName = "";
    
    public void ChangeSceneTo()
    {
        SceneManager.LoadScene(SceneName);
    }
}