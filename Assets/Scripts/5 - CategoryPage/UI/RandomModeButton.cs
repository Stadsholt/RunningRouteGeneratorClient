using UnityEngine;
using UnityEngine.UI;

public class RandomModeButton : MonoBehaviour

{

    public GameObject buttonObject;
    public Button btn;
    public GameObject Target;
    public static bool RandomMode = false;
    public Sprite RandomOn;
    public Sprite RandomOff;

    public void Start()
    {
        RandomMode = false;
        buttonObject = this.gameObject;
        btn = buttonObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
    public void TaskOnClick()
    {
        RandomMode = !RandomMode;

        if (RandomMode == true)
        {
            Target.GetComponent<Image>().sprite = RandomOn;
        }
        if (RandomMode == false)
        {
            Target.GetComponent<Image>().sprite = RandomOff;
        }
    }
}