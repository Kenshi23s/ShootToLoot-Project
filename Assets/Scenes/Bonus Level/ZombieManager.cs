using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FacundoColomboMethods;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;

    [SerializeField] ZombieNav model;
    PoolObject<ZombieNav> zombiePool = new PoolObject<ZombieNav>();

     List<ZombieNav> zombiesList = new List<ZombieNav>();

    Transform[] spawns;

    public int zombiesAlive => zombiesList.Count;
    //public float zombieDamage;

    [SerializeField]
     int maxZombies;
    

    [SerializeField] PlayerEntity Player;
    public Vector3 playerPos => Player.transform.position;

    private void Awake()
    {
        Instance = this;
        spawns = ColomboMethods.GetChildrenComponents<Transform>(transform);
        zombiePool.Intialize(TurnOnZombie, TurnOffZombie, BuildZombie);
        SpawnZombie();
    }

    Vector3 NearestSpawn() => ColomboMethods.CheckNearest<Transform>(spawns, playerPos).position;

    public void SpawnZombie()
    {
        while (zombiesAlive <= maxZombies)
        {
            ZombieNav zombie = BuildZombie();
          
            zombiesList.Add(zombie);
            zombie.transform.position = NearestSpawn();
        }
    }
    
    #region Pool
    void TurnOnZombie(ZombieNav z)
    {
       z.gameObject.SetActive(true);
    }

    void TurnOffZombie(ZombieNav z)
    {
        zombiesList.Remove(z);
        z.gameObject.SetActive(false);
        SpawnZombie();
    }

    ZombieNav BuildZombie()
    {
        ZombieNav zombie = GameObject.Instantiate(model);
        zombie.InitializeZombie(ReturnZombie);
        return zombie;
    }

    void ReturnZombie(ZombieNav z) => zombiePool.Return(z);

    ZombieNav GetEnemy() =>  zombiePool.Get();
    
    
     
    #endregion
}
