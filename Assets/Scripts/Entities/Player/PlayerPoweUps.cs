using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoweUps : MonoBehaviour
{
    //public enum PowerUps
    //{
    //    Healing,
    //    HealingOverTime,
    //    Impulse,
    //    MoreTemporalSpeed,
    //    CantTakeDamage
    //}
    PlayerEntity player;
    bool isOverTime;
    public PlayerPoweUps(PlayerEntity player, bool isOverTime)
    {
        this.player = player;
        this.isOverTime = isOverTime;
    }
    public int OnHeal(int hpToAdd, int actualLife, int maxLife)
    {
        actualLife += hpToAdd;
        if (actualLife >= maxLife)
        {
            actualLife = Mathf.Clamp(actualLife, 0, maxLife);
        }
        return actualLife;
    }
    //public int HealOverTime(int hpToAdd, int actualLife, int maxLife)
    //{
    //    actualLife += hpToAdd;
    //    if (actualLife >= maxLife)
    //    {
    //        actualLife = Mathf.Clamp(actualLife, 0, maxLife);
    //    }
    //    return actualLife;
    //}
    public void PowerUpdates()
    {
        if (isOverTime==true)
        {
            //cosas de update
        }
    }
    //void SelectPowerUp(PlayerPoweUps.PowerUps power)
    //{
    //    switch (power)
    //    {
    //        case PowerUps.Healing:
    //            Heal()
    //            break;
    //        case PowerUps.HealingOverTime:
    //            HealOverTime
    //            break;
    //        case PowerUps.MoreSpeed:
    //            break;
    //        case PowerUps.MoreTemporalSpeed:
    //            break;
    //        case PowerUps.CantTakeDamage:
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
