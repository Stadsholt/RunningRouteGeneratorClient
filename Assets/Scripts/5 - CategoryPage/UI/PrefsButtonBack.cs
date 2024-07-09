using UnityEngine;
using UnityEngine.UI;

public class PrefsButtonBack : MonoBehaviour
{

    public GameObject buttonObject;
    public Button btn;
    public GameObject Target;
    public GameObject btnDisable1;


    public void Start()
    {
        buttonObject = this.gameObject;
        btn = buttonObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
    public void TaskOnClick()
    {
        Target.SetActive(false);
        btnDisable1.SetActive(true);
    }
}