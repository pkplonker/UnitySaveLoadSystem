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
		[SerializeField] private string fileName = "Game.data";
		private ISaveLoadIO output;
		public event Action OnSave;
		public event Action OnLoad;

		private void Awake()
		{
			#region Singleton

			if (instance == null )
			{
				instance = this;
			}
			else if(instance!=this)
			{
				Debug.LogWarning("Destroying" + this + " on gameobject " + gameObject.name + " due to singleton");
				Destroy(this);
			}
			#endregion

			output = new SaveLoadIOMediator().CreateSaveLoadExternal(fileName);
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
			output ??= new SaveFileHandler(Application.persistentDataPath, fileName);
			output.Clear();
		}
		/// <summary>
		/// Method <c>LoadGame</c> Public function to load existing save file
		/// </summary>
		public void LoadGame()
		{
			LoadData(output.Load());
		}
		/// <summary>
		/// Method <c>SaveGame</c> Public function to save new changes
		/// </summary>
		public void SaveGame(bool isNew=false)
		{
			Dictionary<string, object> saveData;
			if (!isNew) saveData = new Dictionary<string, object>();
			else saveData = output.Load() ?? new Dictionary<string, object>();
			SaveData(saveData);
			output.Save(saveData);
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
				if (saveableObject == null)
				{
					saveableObjects.Remove(saveableObject);
					continue;
				}
				if (data.TryGetValue(saveableObject.id, out object saveData))
				{
					saveableObject.LoadState(saveData);
				}
			}
			OnLoad?.Invoke();
		}
		/// <summary>
		/// Method <c>SaveData</c> Private function to collate save data from saveableObjects
		/// </summary>
		private void SaveData(Dictionary<string, object> data)
		{
			foreach (var saveableObject in saveableObjects)
			{
				if (saveableObject == null)
				{
					saveableObjects.Remove(saveableObject);
					continue;
				}
				data[saveableObject.id] = saveableObject.SaveState();
			}
			OnSave?.Invoke();
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