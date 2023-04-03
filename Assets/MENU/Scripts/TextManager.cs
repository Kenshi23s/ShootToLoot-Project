using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI[] textos;

    void Update()
    {
        ShowText();
    }

    void ShowText()
    {
        int line = 2;
        for (int i = 0; i < textos.Length; i++)
        {
            textos[i].text = MenuManager.instance.StringList[line - 2];
            line++;
        }

    }
}
