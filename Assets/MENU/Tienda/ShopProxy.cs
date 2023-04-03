using UnityEngine;


public static class ShopProxy
{
   public static bool CanPurchase(int price,ButtonFlyWeight.CurrencyType currencyType)
   {
		
		if (PlayerPrefs.HasKey("Gems") && PlayerPrefs.HasKey("Coins"))
		{
            int actualCurrencyQuantity;
            string currencyName;

            if (currencyType == ButtonFlyWeight.CurrencyType.Egems)
			{
				actualCurrencyQuantity = PlayerPrefs.GetInt("Gems");
				currencyName = "Gems";

            }
			else
			{
                actualCurrencyQuantity = PlayerPrefs.GetInt("Coins");
                currencyName = "Coins";
            }

			if (actualCurrencyQuantity >= price)
			{
                PlayerPrefs.SetInt(currencyName, actualCurrencyQuantity -= price);
                return true;
			}
			
			
			
		}

		return false;
		
   }
}
