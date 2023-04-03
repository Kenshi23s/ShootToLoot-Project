using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public Sprite Coin, Gem;
    public GameObject settingsOBJ, menuOBJ, storeOBJ, tutorialOBJ;
    public GameObject backButtonSettings, backButtonStore, backButtonTuto;
    public Slider energySlider;

    public static MenuManager instance;
    public enum MenuButtons
    {
        Esettings,
        EmainMenu,
        Estore,
        Etutorial,
        EspinWheel

    }

    public enum BackButton
    {
        EsettingsBack,
        EstoreBack,
        EtutorialBack

    }

    #region LocalizationManager
    public TextAsset archivoTraduccion;

    List<Languages> LanguagesList = new List<Languages>();
    [NonSerialized]
    public List<string> StringList = new List<string>();

    public enum Localization { English, Espaniol }
    public static Localization ActualLanguage; 

    //metodo que deben usarse para cambiar de idioma

    public void Spanish()
    {
        ActualLanguage = Localization.Espaniol;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void English()
    {
        ActualLanguage = Localization.English;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {

        LoadDataCSV();
        SelectLanguage();
    }

    void LoadDataCSV()
    {
        string[] columns = archivoTraduccion.text.Split(new char[] { '\n' });

        for (int i = 1; i < columns.Length - 1; i++)
        {
            string[] row = columns[i].Split(new char[] { ';' });
            Languages L = new Languages();
            L.English = row[0];
            L.Espaniol = row[1];
            LanguagesList.Add(L);
        }

    }

    void SelectLanguage()
    {
        switch (ActualLanguage)
        {
            case Localization.English:
                for (int i = 0; i < LanguagesList.Count; i++)
                {
                    //Guardar lista de strings
                    StringList.Add(LanguagesList[i].English);
                }
                break;

            case Localization.Espaniol:
                for (int i = 0; i < LanguagesList.Count; i++)
                {
                    //Guardar lista de strings
                    StringList.Add(LanguagesList[i].Espaniol);
                }
                break;

        }
    }

    #endregion 



    private void Awake()
    {
      #if UNITY_ANDROID
        ManagerNotifiction.CreateNotification("Che", "The Game:", 20, ManagerNotifiction.TimeScale.Seconds);
      #endif
        //flyWeight Coins
        ButtonFlyWeight.SetSprites(Coin, Gem);
       
        instance = this;    
    }

    

    public void SelectBackbutton(BackButton var)
    {
        switch (var)
        {
            case BackButton.EsettingsBack:
                BackButtonSettings();
                break;
            case BackButton.EstoreBack:
                BackButtonStore();
                break;
            case BackButton.EtutorialBack:
                BackButtonTutorial();
                break;
           
        }
    }

    public void SelectButton(MenuButtons var)
    {
        switch (var)
        {
            case MenuButtons.Esettings:
                Settings();
                break;
            case MenuButtons.Estore:
                Store();
                break;
            case MenuButtons.Etutorial:
                Tutorial();
                break;
            case MenuButtons.EmainMenu:
                MainMenu();
                break;

        }
    }

   #region ButtonsRegion
     public void Settings()
    {
        settingsOBJ.SetActive(true);
        menuOBJ.SetActive(false);
        storeOBJ.SetActive(false);  
        tutorialOBJ.SetActive(false);   
    }

    public void MainMenu()
    {
        settingsOBJ.SetActive(false);
        menuOBJ.SetActive(true);
        storeOBJ.SetActive(false);
        tutorialOBJ.SetActive(false);
    }

    public void Store()
    {
        settingsOBJ.SetActive(false);
        menuOBJ.SetActive(false);
        storeOBJ.SetActive(true);
        tutorialOBJ.SetActive(false);
    }

    public void Tutorial()
    {
        settingsOBJ.SetActive(false);
        menuOBJ.SetActive(false);
        storeOBJ.SetActive(false);
        tutorialOBJ.SetActive(true);
       
    }
    int spintouch;
    public Text text;
    public void Spin()
    {
        spintouch++;
        if (spintouch>=3)
        {
            SpiningManager.instance.StartSpin();
            spintouch = 0;
        }
        Debug.Log("Spin");
        
    }

    public void BackButtonSettings()
    {
        settingsOBJ.SetActive(false);
        menuOBJ.SetActive(true);
        storeOBJ.SetActive(false);
        tutorialOBJ.SetActive(false);
    }

    public void BackButtonStore()
    {
        settingsOBJ.SetActive(false);
        menuOBJ.SetActive(true);
        storeOBJ.SetActive(false);
        tutorialOBJ.SetActive(false);
    }
     
    public void BackButtonTutorial()
    {
        settingsOBJ.SetActive(false);
        menuOBJ.SetActive(true);
        storeOBJ.SetActive(false);
        tutorialOBJ.SetActive(false);
    }

#endregion
     
    public void SetSliderMaxValue(float maxValue)  => energySlider.maxValue = maxValue;

    public void ChangeSliderValue(float newValue)  => energySlider.value = newValue;

    public void MenuManagerLoadLevel(int index) => LoadSceneManager.LoadASyncLevel(index);

   
}
