﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance = null;

	public float volume = 1.0f;

	public int frameRate = 60;

	public Vector3 savedPlayerLocation = Vector3.zero;

	public List<bool> lvlCompletionList;

	public static Game_Manager Instance
	{
		get 
		{
			return instance;
		}
	}
	//Classes created for saving data
	[Serializable]
	class OptionData
	{
		public float volume;
		public int frameRate;
	}

	[Serializable]
	class PlayerData
	{
		public string currentScene;
	}

	[Serializable]
	class GameData
	{
		public List<bool> lvlCompletionList;
	}

	void Awake() 
	{
		//Destroys duplicate Game Managers
		if (instance) 
		{
			DestroyImmediate (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
		ProgressionInit ();
		LoadProgress ();
	}

	void ProgressionInit()
	{
		lvlCompletionList = new List<bool> ();
		for (int i = 0; i < 3; i++) 
		{
			lvlCompletionList.Add (false);
		}
	}

	public void SaveOptions()
	{
		//Creates or overwrites a save file based on the player's options
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream flie = File.Create (Application.persistentDataPath + "/OptionInfoFile.dat");
		OptionData data = new OptionData ();
		data.volume = AudioListener.volume;
		volume = AudioListener.volume;
		data.frameRate = frameRate;

		bf.Serialize (flie, data);
		flie.Close();
	}

	public void LoadOptions()
	{
		//Checks if save file exists
		if (File.Exists (Application.persistentDataPath + "/OptionInfoFile.dat")) 
		{
			//Opens file and sets the values to a class
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/OptionInfoFile.dat", FileMode.Open);
			OptionData data = (OptionData)bf.Deserialize (file);
			file.Close ();

			//Sets class variables to the manager variables
			AudioListener.volume = data.volume;
			frameRate = data.frameRate;
			volume = data.volume;
		}
	}

	public void SavePlayer()
	{
		//Creates or overwrites a save file based on the player's save
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream flie = File.Create (Application.persistentDataPath + "/PlayerInfoFile.dat");
		PlayerData data = new PlayerData ();

		//Sets class variable to the manager variable
		data.currentScene = SceneManager.GetActiveScene ().name;

		bf.Serialize (flie, data);
		flie.Close();
	}

	public void LoadPlayer()
	{
		//Checks if save file exists
		if (File.Exists (Application.persistentDataPath + "/PlayerInfoFile.dat")) 
		{
			//Opens file and sets the values to a class
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfoFile.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			//Starts loading scene from variable
			StartCoroutine (Menu_manager.current.LoadScene (data.currentScene));
		}
	}

	public void SaveProgress()
	{
		//Creates or overwrites a save file based on the player's progress
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream flie = File.Create (Application.persistentDataPath + "/GameProgressionFile.dat");
		GameData data = new GameData ();

		//Sets class variable to the manager variable
		data.lvlCompletionList = lvlCompletionList;

		bf.Serialize (flie, data);
		flie.Close();
	}

	public void LoadProgress()
	{
		//Checks if save file exists
		if (File.Exists (Application.persistentDataPath + "/GameProgressionFile.dat")) 
		{
			//Opens file and sets the values to a class
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/GameProgressionFile.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			//Sets class variables to the manager variables
			lvlCompletionList = data.lvlCompletionList;
			//StartCoroutine (Menu_manager.current.LoadScene (data.currentScene));
		}
	}
}