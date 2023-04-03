using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkinSelect : MonoBehaviour
{
    public Button buttonGold;
    public Button buttonDefault;
    public Text priceText;
    public int price;

    public void SelectGoldSkin()
    {
        
        if (PlayerPrefs.HasKey("Gold"))
        {
            PlayerPrefs.SetString("M4Skin", "Gold");
            buttonGold.interactable = false;
            buttonDefault.interactable = true;
            priceText.text = ":)";
        }
        else
        {
            if (ShopProxy.CanPurchase(price, ButtonFlyWeight.CurrencyType.Egems))
            {
                ManagerPlayerPrefs.instance.SubstractCurrency(0, "Gems");
                PlayerPrefs.SetString("M4Skin", "Gold");
                buttonGold.interactable = false;
                buttonDefault.interactable = true;
                priceText.text = ":)";
            }
         
        }
    }

    public void SelectDefaultSkin()
    {
        PlayerPrefs.SetString("M4Skin", "Default");
        buttonDefault.interactable = false;
        buttonGold.interactable = true;
    }
}
