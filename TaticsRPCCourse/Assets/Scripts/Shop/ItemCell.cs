using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    public const string BuyNotification = "ItemCell.BuyNotification";

    public Item item { get; private set; }
    [SerializeField] Image icon;
    [SerializeField] Text nameLabel;
	[SerializeField] Text atkLabel;
	[SerializeField] Text lvlLabel;
	[SerializeField] Text priceLabel;

	public void Load (Item item)
	{
		this.item = item;
		icon.sprite = item.sprite;
		nameLabel.text = item.name;
		atkLabel.text = string.Format("ATK:{0}", item.attack);
		lvlLabel.text = string.Format("LVL:{0}", item.level);
		priceLabel.text = item.price.ToString();
	}

	public void OnBuyButton ()
	{
		this.PostNotification(BuyNotification);
	}
}
