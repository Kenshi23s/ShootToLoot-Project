using UnityEngine;

public abstract class GameModeBaseClass : MonoBehaviour
{
    

    public static GameModeBaseClass instance;
    [Header("GameModeBaseClass")]
    public TeleportPlatform[] _Tps;
    [SerializeField]
    public float points;

    public void AddPoints(float value)
    {
        points += value;
        points = Mathf.Clamp(points, 0f, pointsToWin);

        if (instance.points >= pointsToWin)
        {
            ModeFinish();
        }
    }

  
    [SerializeField]protected float pointsToWin=5;

    public abstract void InitializeMode();

    private void Awake() => InitializeMode();

   protected virtual void ModeFinish()
   {
        foreach (TeleportPlatform item in _Tps)
            item.ChangeTP_Function(GoToMenus);
        
        
   }

    void GoToMenus() => LoadSceneManager.LoadASyncLevel(0);
}
