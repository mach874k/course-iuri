using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;
    private Player _player;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();

            if(_player != null){
                UIManager.Instance.OpenShop(_player._diamonds);
            }

            shopPanel.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch(item){
            case 0: //flame sword
                UIManager.Instance.UpdateSelection(110);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1: // boots
                UIManager.Instance.UpdateSelection(41);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2: // key
                UIManager.Instance.UpdateSelection(-35);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if(_player._diamonds >= currentItemCost){
            _player._diamonds -= currentItemCost;
            switch(currentSelectedItem){
                case 0: //flame sword
                    break;
                case 1: // boots
                    break;
                case 2: // key
                    GameManager.Instance.HasKeyToCastle = true;
                    break;
            }
            shopPanel.SetActive(false);
        }
        else{
            shopPanel.SetActive(false);
        }
    }
}
