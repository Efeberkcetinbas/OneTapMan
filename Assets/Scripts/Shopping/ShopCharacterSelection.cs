using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShopCharacterSelection : MonoBehaviour
{
    public List<ShopCharacter> shopCharacters=new List<ShopCharacter>();

    public Color color;
    public LevelData levelData;
    public PlayerData playerData;

    private void Start()
    {
        playerData.LoadData();
    }

    public void SelectCharacter(int selectedIndex)
    {
        if(shopCharacters[selectedIndex].button.interactable)
        {
            if(!shopCharacters[selectedIndex].isPurchased)
            {
                levelData.score-=shopCharacters[selectedIndex].price;
                shopCharacters[selectedIndex].shopData.isPurchased=true;
                EventManager.Broadcast(GameEvent.OnCharacterSelected);
                EventManager.Broadcast(GameEvent.OnUIUpdate);
            }
            
            

            for (int i = 0; i < shopCharacters.Count; i++)
            {
                shopCharacters[i].button.image.color=Color.white;
                shopCharacters[i].transform.DOScale(Vector3.one,0.2f);
            }

            shopCharacters[selectedIndex].button.image.color=Color.green;
            shopCharacters[selectedIndex].transform.DOScale(Vector3.one*1.2f,0.2f);
            playerData.selectedCharacterIndex=selectedIndex;
            playerData.SaveData();
            EventManager.Broadcast(GameEvent.OnCharacterChanged);
        }
    }
}
