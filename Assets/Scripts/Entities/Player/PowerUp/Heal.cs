using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PowerUp
{
    int lifeToAdd;
    public override void PowerUpTrigger(PlayerEntity player)
    {
        //player.HealPlayer(lifeToAdd);
    }
}
