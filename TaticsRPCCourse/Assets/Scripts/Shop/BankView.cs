using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BankView : MonoBehaviour 
{
	#region Fields
	[SerializeField] Text label;
	EasingControl ec;
	int startGold;
	int endGold;
	int currentGold;
	#endregion

	#region MonoBehaviour
	void Awake ()
	{
		ec = gameObject.AddComponent<EasingControl>();
		ec.equation = EasingEquations.EaseOutQuad;
		ec.duration = 0.5f;
		startGold = currentGold = endGold = Bank.instance.gold;
		label.text = Bank.instance.gold.ToString();
	}

	void OnEnable ()
	{
		this.AddObserver(OnGoldChanged, Bank.GoldChanged);
		ec.updateEvent += OnEasingUpdate;
	}

	void OnDisable ()
	{
		this.RemoveObserver(OnGoldChanged, Bank.GoldChanged);
		ec.updateEvent -= OnEasingUpdate;
	}
	#endregion

	#region Event Handlers
	void OnGoldChanged (object sender, object args)
	{
		if (ec.IsPlaying)
			ec.Stop();
		startGold = currentGold;
		endGold = Bank.instance.gold;
		ec.SeekToBeginning();
		ec.Play();
	}

	void OnEasingUpdate (object sender, System.EventArgs e)
	{
		if (ec.IsPlaying)
		{
			currentGold = Mathf.RoundToInt((endGold - startGold) * ec.currentValue) + startGold;
			label.text = currentGold.ToString();
		}
	}
	#endregion
}