using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    PoolObject<EnemyEntity> myPool =new PoolObject<EnemyEntity>();

    EnemyEntity model;

    void TurnOnBullet(EnemyEntity b)
    {
        b.gameObject.SetActive(true);
    }

    void TurnOffBullet(EnemyEntity b)
    {
        b.gameObject.SetActive(false);
    }

    EnemyEntity Build()
    {
        EnemyEntity enemy = GameObject.Instantiate(model);
        enemy.InitializeEnemy(ReturnEnemy);
        return enemy;
    }

    void ReturnEnemy(EnemyEntity elem)
    {
        myPool.Return(elem);
    }

    EnemyEntity GetEnemy()
    {
        return myPool.Get();
    }
    
}
