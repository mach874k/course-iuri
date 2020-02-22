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
	[SerializeField] private List<GameObject> droids = new List<GameObject>();

	private int level = 1;
	private string path;
	private void Start() {
		path = Application.persistentDataPath + "/player.dat";
		Load();
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
		InitLevelData();
		Save();
	}

	public void AddDroids(GameObject droid){
		if(droid)
			droids.Add(droid);
		Save();
	}

	private void InitLevelData(){
		level = (xp / levelBase + 1);
		requiredXP = levelBase * level;
	}

	private void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(path);
		PlayerData data = new PlayerData(this);
		bf.Serialize(file, data);
		file.Close();
	}

	private void Load(){
		if(File.Exists(path)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path, FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize(file);
			file.Close();

			xp = data.Xp;
			requiredXP = data.RequiredXp;
			levelBase = data.LevelBase;
			level = data.Level;

			foreach(DroidData droidData in data.Droids){
				if(droidData != null){
					Droid droid = GetComponent<Droid>();
					droid.LoadFromDroidData(droidData);
					AddDroids(droid.gameObject);
				}
			}
		}
		else{
			InitLevelData();
		}
	}
}