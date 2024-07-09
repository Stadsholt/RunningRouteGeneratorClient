using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PrefsSubMenu : MonoBehaviour
{
    public GameObject buttonObject;
    public GameObject Title;
    public Button btn;
    public TMP_Text CategoryText;

    public GameObject ParentObject;


   
    public void Start()
    {
        buttonObject = this.gameObject;
        btn = buttonObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        ParentObject = buttonObject.transform.parent.gameObject;
        CategoryText = ParentObject.GetComponentInChildren<TextMeshProUGUI>();

    }
    public void TaskOnClick()
    {
        //CategoryText
        Title.GetComponent<TextMeshProUGUI>().text = CategoryText.text;

    }
}