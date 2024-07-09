using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PrefsButton : MonoBehaviour
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
        btnDisable1.SetActive(false);
        Target.SetActive(true);

    }
}