
using UnityEngine;
using System.IO;

public static class TextLoad
{
    public static string ReadText(string doc = "en-US")
    {
        string path = Application.persistentDataPath + "/" + doc + ".txt";

        StreamReader reader = new StreamReader(path);
        string txt = reader.ReadToEnd();
        reader.Close();
        return txt;
    }
}


