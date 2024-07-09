using System.IO;
using UnityEngine;

public class WipeHistory : MonoBehaviour
{
    public void DeleteHistory()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "Hist*.txt");

        foreach (string file in files)
        {
            File.Delete(file);
        }
    }
}