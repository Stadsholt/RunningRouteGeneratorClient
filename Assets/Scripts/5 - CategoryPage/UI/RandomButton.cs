using UnityEngine;
using UnityEngine.UI;

public class RandomButton : MonoBehaviour

{

    public GameObject buttonObject;


    public Button btn;

    public GameObject Target;
    public GameObject btnDisable1;


    public void Start()
    {
        Debug.Log("Test");

        //yourButton = this.gameObject;
        buttonObject = this.gameObject;



        btn = buttonObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
    public void TaskOnClick()
    {
        Target.SetActive(false);

    }
}