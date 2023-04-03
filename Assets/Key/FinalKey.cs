using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKey : SuckTowardsTrigger
{
    [SerializeField]
    int coinsToAdd = 20;
    public override void EffectTrigger(Collider other)
    {
        var Player = other.transform.gameObject.GetComponent<PlayerEntity>();

        if (Player != null)
        {
            GameManager.instance.AddCoins(coinsToAdd);
            GameManager.instance.LlavesRestantes--;

            if (GameManager.instance.LlavesRestantes <= 0)
            {
                GameManager.instance.EndLevel(true);
            }

            Destroy(this.gameObject);
        }
    }
}
