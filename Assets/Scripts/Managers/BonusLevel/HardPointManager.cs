using System.Collections;
using UnityEngine;
using FacundoColomboMethods;

public class HardPointManager : GameModeBaseClass
{
   
    HardPoint[] hardPoints;
    [Header("HardPoint")]
    public float speed;
    float VanishTime;
    [SerializeField] float InitialVanishTime;

    public Color neutral, contested, enemyTake, playerTake;
   
    HardPoint point;

    public override void InitializeMode()
    {
   
        instance = this;
        VanishTime = 0;
        hardPoints = ColomboMethods.GetChildrenComponents<HardPoint>(transform);     
        foreach (HardPoint item in hardPoints)
        {
            item.gameObject.SetActive(false);
        }
        int turn = Random.Range(0, hardPoints.Length);
        hardPoints[turn].gameObject.SetActive(true);
        hardPoints[turn].Initialize(neutral, enemyTake,playerTake, contested, speed);
        point = hardPoints[turn];



    }
    protected override void ModeFinish()
    {
        base.ModeFinish();
        Turn();

    }

    void Turn() => StartCoroutine(TurnOffHardPoint());
    IEnumerator TurnOffHardPoint()
    {
       
       
        while (InitialVanishTime > VanishTime)
        {
          
            VanishTime += Time.deltaTime;
            Debug.Log(VanishTime);
            point.Aura.SetFloat("_Alpha", 1 - VanishTime / InitialVanishTime );
            Debug.Log(point.Aura.GetFloat("_Alpha"));

            yield return new WaitForEndOfFrame();

            if (InitialVanishTime < VanishTime)
            {
                Destroy(point.gameObject);
                break;
            }
          


        }




    }

   
}
