using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    private int frameRate = 80;
    FingerManager _fingerManager;
    public HUDconfig UIconfig;
    public static GameManager instance;
   
    [SerializeField]
    GameObject player;
    public GameObject _popUp;

    [NonSerialized]
    public Transform PlayerPos;

    [NonSerialized]
    public int LlavesRestantes;

    #region Input
    public event Action<InputMethod> InitializeController;

    public enum InputMethod
    {
        E_VirtualJoystick,
        E_Joystick

        // E_MouseAndKey
    }
     [SerializeField]
     InputMethod ActualInput;
    #endregion   
    
    [NonSerialized]
    public List<Entity> entities = new List<Entity>(); 
    public List<GameObject> ObserversGameObject=new List<GameObject>();
    HashSet<IObserver> entityObservers = new HashSet<IObserver>();

    public void AddEntity(Entity e)
    {
        entities.Add(e);
        foreach (IObserver Obs in entityObservers)
        {
            e.Subscribe(Obs);
        }
    }

    public void RemoveEntity(Entity e)
    {
        entities.Remove(e);
        foreach (IObserver Obs in entityObservers)
        {
            e.Desubscribe(Obs);
        }
    }

    //se deberia crear una clase aparte para los fingers
    #region Fingers
    List<int> fingersBeingUsed = new List<int>();

    public void AddFingerToList(int _idFinger)
    {
        _fingerManager.AddFingerToList(_idFinger);

    }
    public void RemoveFingerList(int _idFinger)
    {
        _fingerManager.RemoveFingerList(_idFinger);
    }

    public bool CheckFingerUsed(out Touch t)
    {
        return _fingerManager.CheckFingerUsed(out t);


    }
    #endregion//fingers



    void Awake()
    {
        _fingerManager = new FingerManager(fingersBeingUsed);

        Application.targetFrameRate = frameRate;
       
        instance = this;
        #region
        PlayerPos = player.transform;

        for (int i = 0; i < ObserversGameObject.Count; i++)
        {
            var observer = ObserversGameObject[i].GetComponent<IObserver>();
            if (observer != null && entityObservers.Contains(observer) != true)
            {
                entityObservers.Add(observer);
            }
        }
        #endregion//awake things
#if UNITY_ANDROID
     //ActualInput = InputMethod.E_VirtualJoystick;
#endif

        // declaro que este objeto es el gameManager


    }

    private void Start()
    {
        // si alguien se subscribio lo llamo
        if (InitializeController!=null)
        {
            InitializeController?.Invoke(ActualInput);
        }
        else
        {
            Debug.LogWarning("No se pudo inicializar control");
        }
       
    }



    void LateUpdate()
    {
        PlayerPos = player.transform;
    }

   
    IEnumerator WaitFrame()
    {
        yield return new WaitForEndOfFrame();
    }


    //habria que crear un scene manager para estos metodos
    #region SceneLoad

    public void PopUpSwitch(bool value)
    {
       _popUp.SetActive(value);
        Debug.Log("dfsdfsdf");


    } 



    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        LoadSceneManager.LoadASyncLevel(0);
    }

    public void GoToLevelMenu()
    {

        LoadSceneManager.LoadASyncLevel(1);
    }

    public void LoadScene(string SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void LoadScene(int SceneToLoad)
    {
        LoadSceneManager.LoadASyncLevel(SceneToLoad);
    }

    public void EndLevel(bool is_Win)
    {
        if (is_Win == true)
        {
            LevelsManager.UnlockNextLevel();  //desbloquear sabiendo el flyweight

            AddCurrency(LevelGems, "Gems");
            AddCurrency(LevelCoins, "Coins");
        }
        GoToLevelMenu();

    }

#endregion

    //habria que crear un playerPrefs manager para estos metodos

    int LevelGems = 0;
    int LevelCoins = 0;

    public void AddGems(int addGems)
    {
        LevelGems+=addGems;
        UIconfig.GemsUIUpdate(LevelGems);
    }

    public void AddCoins(int addCoins)
    {
        LevelCoins += addCoins;
        UIconfig.CoinsUIUpdate(LevelCoins);
    }

    void AddCurrency(int valueToAdd, string key)
    {
        int actualValue;

        if (PlayerPrefs.HasKey(key) != false)
        {
            actualValue = PlayerPrefs.GetInt(key);
            actualValue += valueToAdd;
            PlayerPrefs.SetInt(key, actualValue);

        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
            AddCurrency(valueToAdd, key);
        }

    }

    public void InstantiateCubeForTest(Vector3 pos)
    {
        GameObject test = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), pos, Quaternion.identity);
    }

}
