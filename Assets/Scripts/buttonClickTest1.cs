using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class buttonClickTest1 : MonoBehaviour
{
    public string SceneName = "";
    public GameObject buttonObject;
    public Button btn;

    public void Start()
    {
        buttonObject = this.gameObject;
        btn = buttonObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
    public void TaskOnClick()
    {
        SceneManager.LoadScene(SceneName);
    }
}