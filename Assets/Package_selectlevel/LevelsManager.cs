using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager instance;
    public List<Level> Levels;
    public StaminaPlay stamina;
    public GameObject Line;

    static int UnlockedLevels;

    public Animator FadeLoadAnim;

    private void Awake()
    {
        CreateLinesLevels();
        UnlockedLevels = GetUnlockedLevels();

        for (int i = 0; i < Levels.Count; i++)
        { 
            if (UnlockedLevels <= i)
            {
                Levels[i].MainLevelData.Locked = true;
            } 
            else
            {
                Levels[i].MainLevelData.Locked = false;
            }
            

            Levels[i].ActualizarLevel();
        }


        instance = this;

    }


    void Start()
    {
     
        
    }

    public void CreateLinesLevels()
    {
        print(Levels.Count);

        for (int i = 0; i < Levels.Count-1; i++)
        {
            print("linea = " + i);

            GameObject CurrentLine = Instantiate(Line,Levels[i].transform.position,Levels[i].transform.rotation);

            LineRenderer CurrentLineRender = CurrentLine.GetComponent<LineRenderer>();

            CurrentLineRender.SetPosition(0, Levels[i].transform.position + new Vector3(0, -1, 1));
            CurrentLineRender.SetPosition(1, Levels[i+1].transform.position + new Vector3(0, -1, 1));

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < Levels.Count - 1; i++)
        {
            Gizmos.DrawLine(Levels[i].transform.position, Levels[i + 1].transform.position);
        }

    }

    public void SetLevelsNames()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].MainLevelData.NameLevel = (i+1).ToString();
        }
    }


    public void GoToLevel(LevelData MainLevelData)
    {
        if (stamina.OnPlayButton() == true)
        {
            MapCreatorManager.SetParameters(MainLevelData);
            LoadSceneManager.LoadASyncLevel(2);

            Debug.LogError("CON ENERGIA, ME QUEDA ( " + stamina._stamina + " )");
        }
        else
        {
            MapCreatorManager.SetParameters(MainLevelData);
            LoadSceneManager.LoadASyncLevel(2);

            Debug.LogError("SIN ENERGIA, AUN ASI ENTRA AL LEVEL PARA DEBUGEAR");
        }
    }

    public void GoToLevel()
    {
        if (stamina.OnPlayButton() == true)
        {
            LoadSceneManager.LoadASyncLevel(2);

            Debug.LogError("CON ENERGIA, ME QUEDA ( " + stamina._stamina + " )");
        }
        else
        {

            LoadSceneManager.LoadASyncLevel(2);

            Debug.LogError("SIN ENERGIA, AUN ASI ENTRA AL LEVEL PARA DEBUGEAR");
        }
    }

    public void BackToLevel()
    {
        LoadSceneManager.LoadASyncLevel(1);
    }

    public static void UnlockNextLevel()
    {
        int temp = PlayerPrefs.GetInt("UnlockedLevels") + 1;
        PlayerPrefs.SetInt("UnlockedLevels",temp);
    }


    public int GetUnlockedLevels()
    {
        if (PlayerPrefs.HasKey("UnlockedLevels"))
        {
            return PlayerPrefs.GetInt("UnlockedLevels");
        } 
        else
        {
            PlayerPrefs.SetInt("UnlockedLevels",1);
            return 1;
        }
    }

    public void BackToPrincipalMenu()
    {
        LoadSceneManager.LoadASyncLevel(0);
    }
}
