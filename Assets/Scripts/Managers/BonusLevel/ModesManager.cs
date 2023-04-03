
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModesManager : MonoBehaviour
{
    public static ModesManager instance;
    [SerializeField] GameModeBaseClass[] AvailableGameModes;
    [NonSerialized]
    public GameModeBaseClass gameMode;
    public enum GameMode
    {
        
        EkillConfirm,
        EhardPoint,
        Erandom
    }
    [SerializeField]
    GameMode actualGameMode; 

    GameMode RandomGameMode()
    {
        //GameMode randomGameMode= Random.Range(0, Enum.GetValues(GameMode.).Length+1);
        return GameMode.EhardPoint;
    }
    private void Awake()
    {
       if (actualGameMode == GameMode.Erandom)
       {
           actualGameMode = RandomGameMode();
       }

       gameMode = SelectGameMode();

       gameMode.InitializeMode();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadSceneManager.LoadASyncLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }

    GameModeBaseClass SelectGameMode()
    {
        switch (actualGameMode)
        {
            case GameMode.EkillConfirm:
               return SearchGameMode<KillConfirmManager>();
               
            case GameMode.EhardPoint:
                return SearchGameMode<HardPointManager>();

        }
        return null;
    }
    GameModeBaseClass SearchGameMode<T>() where T : GameModeBaseClass
    {
        foreach (T gameModeClass in AvailableGameModes)
        {
            return gameModeClass;
        }
        return null;
    }

}
