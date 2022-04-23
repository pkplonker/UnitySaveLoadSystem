using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
	public class SaveableGameObject : MonoBehaviour, ISerializationCallbackReceiver
	{
		public string id;

		private void OnEnable()
		{
			if (SavingSystem.instance == null) return;
			SavingSystem.instance.Subscribe(this);
		}

		private void OnDisable()
		{
			if (SavingSystem.instance == null) return;
			SavingSystem.instance.UnSubscribe(this);
		}


		public void OnBeforeSerialize()
		{
			GenerateUniqueID();
		}

		/// <summary>
		/// Method <c>GenerateUniqueID</c> Private function to generate unique ID if one does not exist
		/// </summary>
		private void GenerateUniqueID()
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				id = Guid.NewGuid().ToString();
			}
		}


		//required for ISerializationCallbackReceiver interface
		public void OnAfterDeserialize()
		{
		}

		/// <summary>
		/// Method <c>SaveState</c> Public function that gets all components implementing ISaveLoad on this object add adds saveData to return dictionary
		/// </summary>
		public Dictionary<string, object> SaveState()
		{
			var saveData = new Dictionary<string, object>();
			foreach (var component in GetComponents<ISaveLoad>())
			{
				saveData[component.GetType().ToString()] = component.SaveState();
			}

			return saveData;
		}

		/// <summary>
		/// Method <c>LoadState</c> Public function that gets all components implementing ISaveLoad on this object add disseminates saveData to them
		/// </summary>
		public void LoadState(object data)
		{
			var saveData = (Dictionary<string, object>) data;
			foreach (var component in GetComponents<ISaveLoad>())
			{
				string typeName = component.GetType().ToString();
				if (saveData.TryGetValue(typeName, out object componentSaveData))
				{
					component.LoadState(componentSaveData);
				}
			}
		}
	}
}