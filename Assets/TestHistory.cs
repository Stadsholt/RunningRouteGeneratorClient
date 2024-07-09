using System.IO;
using UnityEngine;
using TMPro;
public class TestHistory : MonoBehaviour
{
    [SerializeField] private TMP_Text Textbox;

    private int number=0;
    public void DeleteHistory()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "Hist*.txt");

        foreach (string file in files)
        {
            var num2 = number + 1;
            Textbox.SetText(num2.ToString());
        }
    }
}