using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemShop : MonoBehaviour 
{
	#region Consts
	public const string BuyNotification = "ItemShop.BuyNotification";
	const string cellKey = "ItemShop.cellPrefab";
	#endregion

	#region Fields
	[SerializeField] GameObject cellPrefab;
	[SerializeField] Transform content;
	List<Item> items;
	List<Poolable> cells;
	#endregion

	#region MonoBehaviour
	void OnEnable ()
	{
		this.AddObserver(OnBuyItemNotification, ItemCell.BuyNotification);
	}

	void OnDisable ()
	{
		this.RemoveObserver(OnBuyItemNotification, ItemCell.BuyNotification);
	}
	#endregion

	#region Public
	public void Load (List<Item> items)
	{
		if (cells == null)
		{
			GameObjectPoolController.AddEntry(cellKey, cellPrefab, items.Count, int.MaxValue);
			cells = new List<Poolable>(items.Count);
		}

		this.items = items;
		Reload();
	}

	public void Reload ()
	{
		DequeueCells(items);
	}
	#endregion

	#region Event Handlers
	void OnBuyItemNotification (object sender, object args)
	{
		ItemCell cell = sender as ItemCell;
		if (Bank.instance.gold >= cell.item.price)
			Purchase(cell.item);
		else
			GetComponent<DialogController>().Show("Need Gold!", "You don't have enough gold to complete this purchase.  Would you like to buy more?", FakeBuyGold, null);
	}

	public void OnSortByName ()
	{
		items.Sort( delegate(Item x, Item y) {
			return x.name.CompareTo(y.name);
		} );
		Reload();
	}

	public void OnSortByPrice ()
	{
		items.Sort( delegate(Item x, Item y) {
			return x.price.CompareTo(y.price);
		});
		Reload();
	}

	public void OnSortByAttack ()
	{
		items.Sort( delegate(Item x, Item y) {
			return y.attack.CompareTo(x.attack);
		});
		Reload();
	}

	public void OnSortByLevel ()
	{
		items.Sort( delegate(Item x, Item y) {
			return x.level.CompareTo(y.level);
		});
		Reload();
	}
	#endregion

	#region Private
	void FakeBuyGold ()
	{
		Bank.instance.gold += 5000;
	}

	void Purchase (Item item)
	{
		Bank.instance.gold -= item.price;
		this.PostNotification(ItemShop.BuyNotification, item);
	}

	void EnqueueCells ()
	{
		for (int i = cells.Count - 1; i >= 0; --i)
			GameObjectPoolController.Enqueue(cells[i]);
		cells.Clear();
	}

	void DequeueCells (List<Item> items)
	{
		EnqueueCells();
		if (items == null)
			return;

		for (int i = 0; i < items.Count; ++i)
		{
			Poolable obj = GameObjectPoolController.Dequeue(cellKey);
			obj.GetComponent<ItemCell>().Load(items[i]);
			obj.transform.SetParent(content);
			obj.gameObject.SetActive(true);
			cells.Add(obj);
		}
	}
	#endregion
}