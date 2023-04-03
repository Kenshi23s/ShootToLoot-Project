using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    //el personaje nunca se destruye ni se crea, solo se transforma (materia)

    public PlayerEntity Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>();
        
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayer()
    {
        if (Player != null)
        {
            Player.transform.position = this.transform.position;
            Player.life = 100;
        } else
        {
            print("El spawn no contiene nignun personaje");
        }
    }
}
