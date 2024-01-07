using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCharacter : MonoBehaviour
{
    public int price;

    public bool isPurchased=false;
    public bool canBuy=false;

    public Image characterImage;

    internal Button button;

    public TextMeshProUGUI priceText;

    public Color color;

    public ShopData shopData;
    public LevelData levelData;

    private void Start()
    {
        button=GetComponent<Button>();
        priceText.SetText(price.ToString());
        CheckPurchase();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);
        
    }

    private void OnCharacterSelected()
    {
        CheckPurchase();
    }

    private void OnShopOpen()
    {
        CheckPurchase();
    }

    private void CheckPurchase()
    {
        if(shopData.isPurchased)
        {
            button.interactable=true;
            priceText.gameObject.SetActive(false);
            isPurchased=true;
        }

        if(levelData.score>=price || shopData.isPurchased)
        {
            button.interactable=true;
            canBuy=true;
        }

        if(!shopData.isPurchased)
        {
            if(levelData.score<price)
            {
                button.interactable=false;
                canBuy=false;
            }
        }
    }
}
