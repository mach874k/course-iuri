using UnityEngine;
using System.Collections;

public class Bank 
{
	#region Consts
	public const string GoldChanged = "Bank.GoldChanged";
	const string GoldKey = "Bank.GoldKey";
	#endregion

	#region Fields
	public int gold
	{
		get { return _gold; }
		set 
		{
			if (_gold == value)
				return;
			_gold = value;
			Save ();
			this.PostNotification(GoldChanged);
		}
	}
	private int _gold;
	#endregion

	#region Singleton
	public static readonly Bank instance = new Bank();
	private Bank () 
	{ 
		Load(); 
	}
	#endregion

	#region Private
	void Load ()
	{
		_gold = PlayerPrefs.GetInt(GoldKey, 5000);
	}

	void Save ()
	{
		PlayerPrefs.SetInt(GoldKey, _gold);
	}
	#endregion
}