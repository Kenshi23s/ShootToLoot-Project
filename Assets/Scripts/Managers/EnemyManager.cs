using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   public List<EnemyEntity> enemies=new List<EnemyEntity>();
   public static EnemyManager instance;

   private void Awake()
   {
       instance = this;
   }


    public void AddToEnemyList(EnemyEntity e)
    {
        enemies.Add(e);
       
    }
    public void RemoveFromEmeyList(EnemyEntity e)
    {
        enemies.Remove(e);
    }
}
