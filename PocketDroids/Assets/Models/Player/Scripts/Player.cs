using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private int xp = 0;
	[SerializeField] private int requiredXP = 100;
	[SerializeField] private int levelBase = 100;
	[SerializeField] private int level = 1;
	[SerializeField] private List<GameObject> droids = new List<GameObject>();

	private void Start() {
		InitLevelData();
	}
	public int Xp {
		get { return xp; }
	}

	public int RequiredXP {
		get { return requiredXP; }
	}

	public int LevelBase {
		get { return levelBase; }
	}
	
	public int Level {
		get { return level; }
	}

	public List<GameObject> Droids {
		get { return droids; }
	}

	public void AddXP(int xp){
		this.xp += Mathf.Max(0, xp);
	}

	public void AddDroids(GameObject droid){
		droids.Add(droid);
	}

	private void InitLevelData(){
		level = (xp / levelBase + 1);
		requiredXP = levelBase * level;
	}

}