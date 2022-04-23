using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Save
{
	public class SavingSystem : MonoBehaviour
	{
		public static SavingSystem instance { get; private set; }
		private List<SaveableGameObject> saveableObjects;
		[SerializeField] private string fileName;
		private SaveFileHandler saveFileHandler;

		private void Awake()
		{
			#region Singleton
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(this);
			}
			#endregion
			saveFileHandler = new SaveFileHandler(Application.persistentDataPath, fileName);
		}

		private void OnEnable()
		{
			saveableObjects = new List<SaveableGameObject>();
		}

		private void Start()
		{
			LoadGame();
		}
		/// <summary>
		/// Method <c>ClearSave</c> Public function to clear existing save file
		/// </summary>
		public void ClearSave()
		{
			saveFileHandler ??= new SaveFileHandler(Application.persistentDataPath, fileName);
			saveFileHandler.Clear();
		}
		/// <summary>
		/// Method <c>LoadGame</c> Public function to load existing save file
		/// </summary>
		public void LoadGame()
		{
			LoadData(saveFileHandler.Load());
		}
		/// <summary>
		/// Method <c>SaveGame</c> Public function to save new changes
		/// </summary>
		public void SaveGame(bool isNew=false)
		{
			Dictionary<string, object> saveData;
			if (!isNew) saveData = new Dictionary<string, object>();
			else saveData = saveFileHandler.Load() ?? new Dictionary<string, object>();
			SaveData(saveData);
			saveFileHandler.Save(saveData);
		}

		private void OnApplicationQuit()
		{
			SaveGame();
		}
		/// <summary>
		/// Method <c>LoadData</c> Private function to distribute data to saveableobjects
		/// </summary>
		private void LoadData(Dictionary<string, object> data)
		{
			foreach (var saveableObject in saveableObjects)
			{
				if (data.TryGetValue(saveableObject.id, out object saveData))
				{
					saveableObject.LoadState(saveData);
				}
			}
		}
		/// <summary>
		/// Method <c>SaveData</c> Private function to collate save data from saveableObjects
		/// </summary>
		private void SaveData(Dictionary<string, object> data)
		{
			foreach (var saveableObject in saveableObjects)
			{
				data[saveableObject.id] = saveableObject.SaveState();
			}
		}

		/// <summary>
		/// Method <c>Subscribe</c> Public observer pattern subscription
		/// </summary>
		public void Subscribe(SaveableGameObject saveLoadInterface)
		{
			if (!saveableObjects.Contains(saveLoadInterface))
			{
				saveableObjects.Add(saveLoadInterface);
			}
		}
		/// <summary>
		/// Method <c>UnSubscribe</c> Public observer pattern unsubscription
		/// </summary>
		public void UnSubscribe(SaveableGameObject saveLoadInterface)
		{
			if (saveableObjects.Contains(saveLoadInterface))
			{
				saveableObjects.Remove(saveLoadInterface);
			}
		}
	}
}