using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
   [SerializeField] public PoolObject<MyBullet> BulletPool = new PoolObject<MyBullet>();
  
    public MyBullet model;

    public static BulletManager instance;
    
    public int prewarm = 10;


    private void Start()
    {
        InitializePool();
        instance = this;
      
    }
    public void InitializePool()
    {
        BulletPool.Intialize(TurnOnBullet, TurnOffBullet, Build, prewarm);
    }
    
    public void SpawnBullet(Transform SpawnPoint, Quaternion dir, int dmg, float speed)
    {
        MyBullet b = GetBullet();
        
        if (b != null)
        {            
            TurnOnBullet(b);
            b.AssignValues(SpawnPoint, dir, dmg, speed);
            b.ProjectileLaunch();
        }        

    }

    void TurnOnBullet(MyBullet b)
    {
        b.gameObject.SetActive(true);
    }

    void TurnOffBullet(MyBullet b)
    {
        b.gameObject.SetActive(false);
    }

    MyBullet Build()
    {
        MyBullet bullet = GameObject.Instantiate(model);
        bullet.Configure(ReturnBullet);
        bullet.transform.SetParent(this.transform);
        return bullet;
    }

    void ReturnBullet(MyBullet b)
    {
        BulletPool.Return(b);
    }

    MyBullet GetBullet()
    {
        return BulletPool.Get();
    }
   



}
