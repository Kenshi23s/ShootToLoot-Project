using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ShopButton : MonoBehaviour
{
    public ButtonFlyWeight.CurrencyType exchangeType;
    [SerializeField] public Image myImage;
    [SerializeField] Image SplashArt;
    [SerializeField] Image currencyImage;
    [SerializeField] Text priceText;
    [SerializeField] Sprite SplashArtSprite;
    [SerializeField] UnityEvent buyEvent;
    [SerializeField] GameObject sureText;



    [SerializeField, Range(0, 100)] int price;




    // inicializacion en awake ,comunicacion en start.
    private void Awake()
    {
        priceText.text = Mathf.Abs(price).ToString();
        SplashArt.sprite= SplashArtSprite;
        myImage=this.GetComponent<Image>();
    }
    private void Start() => currencyImage.sprite = ButtonFlyWeight.GetSprite(exchangeType);

    int touch = 0;
    //se llamaria por al tocar
    public void OnInteraction()
    {
        touch++;       
        if (ShopProxy.CanPurchase(price, exchangeType))
        {
           touch++;
           if (touch > 3)
           {
              buyEvent.Invoke();
              ManagerPlayerPrefs.UpdateCurrencys();
              sureText.SetActive(false);
              return;
           }
           
        }
        else
        {
            StartCoroutine(ChangeColor());
            sureText.SetActive(false);
            return;
        }
           
        
        sureText.SetActive(true);







    }
    
    IEnumerator ChangeColor()
    {
        UnityEngine.Color original = myImage.color;
        myImage.color = UnityEngine.Color.red;
        yield return new WaitForSeconds(3f);
        myImage.color = original;
    }

    public void BuyCoins()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", coins + 300);
        ManagerPlayerPrefs.UpdateCurrencys();


    }
}
