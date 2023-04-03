using System.Collections.Generic;
using UnityEngine;
using System;

public class HardPoint : MonoBehaviour
{
    List<ZombieNav> enemyEntities=new List<ZombieNav>();

    PlayerEntity playerEntity;
    public Material Aura;
    float speed;
    Color neutral,enemyTake,playerTake,contest;


    public void Initialize(Color neutral, Color enemyTake, Color playerTake, Color contest, float speed)
    {
        this.neutral = neutral;
        this.enemyTake = enemyTake;
        this.playerTake = playerTake;
        this.contest = contest;       
        this.speed = speed;
       

        Aura = this.GetComponent<Renderer>().material;
        Aura.SetFloat("_Alpha", 1);

        CheckWhosInside();
    }

    void ChangeColor(Color actual)
    {
      
        Aura.SetColor("_Color", actual); 
    } 

    private void LateUpdate()
    {
        CheckWhosInside();
      
    }

    private void OnTriggerEnter(Collider other)
    {
       var entity = other.gameObject.GetComponent<Entity>();
        if (entity!=null)
        {
            var player=entity.GetComponent<PlayerEntity>();
            if (player!=null)
            {
                Debug.Log("player");
                playerEntity = player;
            }
            var enemy = other.gameObject.GetComponent<ZombieNav>();
            if (enemy != null && !enemyEntities.Contains(enemy))
            {
                enemy.SetHardpoint(this);
                enemyEntities.Add(enemy);
            }
        }
        
       
    }

   

    public void OnTriggerExit(Collider other)
    {
        var Player = other.gameObject.GetComponent<Entity>();
        if (Player == playerEntity)
        {
            playerEntity = null;
        }

        var enemy = other.gameObject.GetComponent<ZombieNav>();

        if (enemy != null && enemyEntities.Contains(enemy))
        {
            Debug.Log("removi un enemigo");
            enemy.SetHardpoint(null);
            enemyEntities.Remove(enemy);
        }

    }

    void CheckWhosInside()
    {
        
        if (playerEntity != null && enemyEntities.Count >= 1)
        {           
            ChangeColor(contest);
            return;
        }
        if (playerEntity == null && enemyEntities.Count >= 1)
        {
            
            ChangeColor(enemyTake);
            GameModeBaseClass.instance.AddPoints(-(Time.deltaTime * enemyEntities.Count));
            return;
        }
        if (playerEntity != null && enemyEntities.Count <= 0)
        {
            GameModeBaseClass.instance.AddPoints(Time.deltaTime * speed);
            ChangeColor(playerTake);
            return;
        }
        if (playerEntity == null && enemyEntities.Count <= 0)
        {
            ChangeColor(neutral);
        }
    }
}
