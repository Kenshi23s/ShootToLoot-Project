using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTuto : MonoBehaviour
{
    public MenuManager menuManager;
    public List<GameObject> imagesTutorial = new List<GameObject>();
    int index = 0;

    public void SkipImage()
    {
        index++;

        if(index >= imagesTutorial.Count)
        {
            index = 0;        
            menuManager.BackButtonTutorial();
            LoadSceneManager.LoadASyncLevel(4);
        }

        ChangeImages();
    }

    public void ChangeImages()
    {
        for (int i = 0; i < imagesTutorial.Count; i++)
        {
            if(index == i)
            {
                imagesTutorial[i].SetActive(true);
            }
            else
            {
                imagesTutorial[i].SetActive(false);
            }
        }
    }

}
