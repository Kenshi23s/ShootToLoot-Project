using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButtonFlyWeight
{
    public static Sprite Coin, Gem; 
    public enum CurrencyType
    {
        Ecoins,
        Egems
    }
   public static void SetSprites (Sprite _Coin, Sprite _Gem)
   {
        Coin = _Coin;
        Gem =  _Gem;
   }
   
  public static Sprite GetSprite(CurrencyType actual)
  {
     switch (actual)
     {
         case CurrencyType.Ecoins:

             return Coin;
             
         case CurrencyType.Egems:

             return Gem;
                  
     }
     return null;
  }
    
   
}
